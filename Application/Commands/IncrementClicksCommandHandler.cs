using Microsoft.Extensions.Logging;
using Shortly.Application.DTOs;
using Shortly.Application.Interfaces;

namespace Shortly.Application.Commands;

public sealed class IncrementClicksCommandHandler
{
    private readonly ILinkWriteRepository _linkRepository;
    private readonly ILinkReadRepository _readRepository;
    private readonly ILogger<IncrementClicksCommandHandler> _logger;

    public IncrementClicksCommandHandler(
        ILinkWriteRepository linkRepository,
        ILinkReadRepository readRepository,
        ILogger<IncrementClicksCommandHandler> logger)
    {
        _linkRepository = linkRepository ?? throw new ArgumentNullException(nameof(linkRepository));
        _readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
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

        var readModel = await _readRepository.GetByIdAsync(command.LinkId);
        if (readModel is not null)
        {
            readModel.Clicks = link.Clicks;
            await _readRepository.UpdateAsync(readModel);
        }

        _logger.LogInformation("Clicks incremented for linkId: {LinkId}. Total clicks: {Clicks}.", link.Id, link.Clicks);
        return LinkResponse.From(link);
    }
}
