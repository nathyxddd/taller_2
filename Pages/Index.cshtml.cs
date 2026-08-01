using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shortly.Application.Commands;
using Shortly.Application.DTOs;
using Shortly.Application.Queries;

namespace Shortly.Pages;

public class IndexModel : PageModel
{
    private readonly GetLinksByUserIdQueryHandler _getLinksByUserIdQueryHandler;
    private readonly CreateLinkCommandHandler _createLinkCommandHandler;

    public IndexModel(GetLinksByUserIdQueryHandler getLinksByUserIdQueryHandler, CreateLinkCommandHandler createLinkCommandHandler)
    {
        _getLinksByUserIdQueryHandler = getLinksByUserIdQueryHandler;
        _createLinkCommandHandler = createLinkCommandHandler;
    }

    [BindProperty]
    [Required]
    [Url]
    public string OriginalUrl { get; set; } = null!;

    public List<LinkResponse> Links { get; set; } = new();

    public async Task OnGetAsync()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim is not null && long.TryParse(userIdClaim, out var userId))
            {
                Links = await _getLinksByUserIdQueryHandler.HandleAsync(new GetLinksByUserIdQuery(userId));
            }
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (User.Identity?.IsAuthenticated != true)
            return Challenge();

        if (!ModelState.IsValid)
            return Page();

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim is null || !long.TryParse(userIdClaim, out var userId))
            return Challenge();

        await _createLinkCommandHandler.HandleAsync(new CreateLinkCommand(OriginalUrl, userId));
        return RedirectToPage();
    }
}
