using Microsoft.Extensions.Logging;
using Shortly.Application.DTOs;
using Shortly.Application.Interfaces;
using Shortly.Domain.Entities;

namespace Shortly.Application.Commands;

public sealed class CreateLinkCommandHandler
{
    private readonly ILinkWriteRepository _linkRepository;
    private readonly ILinkReadRepository _readRepository;
    private readonly ILogger<CreateLinkCommandHandler> _logger;

    public CreateLinkCommandHandler(
        ILinkWriteRepository linkRepository,
        ILinkReadRepository readRepository,
        ILogger<CreateLinkCommandHandler> logger)
    {
        _linkRepository = linkRepository ?? throw new ArgumentNullException(nameof(linkRepository));
        _readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<LinkResponse> HandleAsync(CreateLinkCommand command)
    {
        _logger.LogDebug("Creating link for URL: {Url} and userId: {UserId}", command.Url, command.UserId);

        var shortUrl = System.Ulid.NewUlid().ToString()[..12].ToLowerInvariant();
        var link = new Link(command.Url, shortUrl, command.UserId);

        await _linkRepository.AddAsync(link);
        await _linkRepository.SaveChangesAsync();

        var readModel = new LinkReadModel(link.Id, link.Url, link.ShortUrl, link.Clicks, link.UserId);
        await _readRepository.AddAsync(readModel);

        _logger.LogInformation("Link created successfully with shortUrl: {ShortUrl} and id: {Id}.", link.ShortUrl, link.Id);
        return LinkResponse.From(link);
    }
}
