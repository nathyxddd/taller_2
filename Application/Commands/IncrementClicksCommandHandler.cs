using Microsoft.Extensions.Logging;
using Shortly.Application.DTOs;
using Shortly.Application.Interfaces;

namespace Shortly.Application.Commands;

public sealed class IncrementClicksCommandHandler
{
    private readonly ILinkWriteRepository _linkRepository;
    private readonly ILogger<IncrementClicksCommandHandler> _logger;

    public IncrementClicksCommandHandler(ILinkWriteRepository linkRepository, ILogger<IncrementClicksCommandHandler> logger)
    {
        _linkRepository = linkRepository ?? throw new ArgumentNullException(nameof(linkRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<LinkResponse> HandleAsync(IncrementClicksCommand command)
    {
        _logger.LogDebug("Incrementing clicks for linkId: {LinkId}", command.LinkId);

        var link = await _linkRepository.GetByIdAsync(command.LinkId);
        if (link is null)
        {
            _logger.LogWarning("IncrementClicks failed: No link found with id {LinkId}.", command.LinkId);
            throw new KeyNotFoundException($"No link found with id '{command.LinkId}'.");
        }

        link.IncrementClicks();
        await _linkRepository.SaveChangesAsync();

        _logger.LogInformation("Clicks incremented for linkId: {LinkId}. Total clicks: {Clicks}.", link.Id, link.Clicks);
        return LinkResponse.From(link);
    }
}
