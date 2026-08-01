using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shortly.Application.Commands;
using Shortly.Application.DTOs;
using Shortly.Application.Interfaces;

namespace Shortly.Pages;

public class IndexModel : PageModel
{
    private readonly ILinkService _linkService;
    private readonly CreateLinkCommandHandler _createLinkCommandHandler;

    public IndexModel(ILinkService linkService, CreateLinkCommandHandler createLinkCommandHandler)
    {
        _linkService = linkService;
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
                Links = await _linkService.GetLinksByUserId(userId);
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
