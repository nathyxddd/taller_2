using Shortly.Application.Commands;
using Shortly.Application.Queries;

namespace Shortly.Endpoints;

public static class UrlRedirectEndpoint
{
    public static void MapUrlRedirect(this WebApplication app)
    {
        app.MapGet("/{shortUrl}", async (string shortUrl, GetLinkByShortUrlQueryHandler getLinkByShortUrlQueryHandler, IncrementClicksCommandHandler incrementClicksHandler) =>
        {
            var link = await getLinkByShortUrlQueryHandler.HandleAsync(new GetLinkByShortUrlQuery(shortUrl));
            if (link is null)
            {
                return Results.NotFound();
            }

            await incrementClicksHandler.HandleAsync(new IncrementClicksCommand(link.Id));
            return Results.Redirect(link.Url);
        });
    }
}
