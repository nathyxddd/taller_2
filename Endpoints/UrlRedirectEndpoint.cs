using Shortly.Application.Commands;
using Shortly.Application.Interfaces;

namespace Shortly.Endpoints;

public static class UrlRedirectEndpoint
{
    public static void MapUrlRedirect(this WebApplication app)
    {
        app.MapGet("/{shortUrl}", async (string shortUrl, ILinkService linkService, IncrementClicksCommandHandler incrementClicksHandler) =>
        {
            try
            {
                var link = await linkService.GetLink(shortUrl);
                await incrementClicksHandler.HandleAsync(new IncrementClicksCommand(link.Id));
                return Results.Redirect(link.Url);
            }
            catch (KeyNotFoundException)
            {
                return Results.NotFound();
            }
        });
    }
}
