using Microsoft.Extensions.Logging;
using Shortly.Application.DTOs;
using Shortly.Application.Interfaces;
using Shortly.Domain.Entities;

namespace Shortly.Application.Commands;

public sealed class CreateLinkCommandHandler
{
    private readonly ILinkWriteRepository _linkRepository;
    private readonly ILogger<CreateLinkCommandHandler> _logger;

    public CreateLinkCommandHandler(ILinkWriteRepository linkRepository, ILogger<CreateLinkCommandHandler> _logger)
    {
        this._linkRepository = linkRepository ?? throw new ArgumentNullException(nameof(linkRepository));
        this._logger = _logger ?? throw new ArgumentNullException(nameof(_logger));
    }

    public async Task<LinkResponse> HandleAsync(CreateLinkCommand command)
    {
        _logger.LogDebug("Creating link for URL: {Url} and userId: {UserId}", command.Url, command.UserId);

        var shortUrl = System.Ulid.NewUlid().ToString()[..12].ToLowerInvariant();
        var link = new Link(command.Url, shortUrl, command.UserId);

        await _linkRepository.AddAsync(link);
        await _linkRepository.SaveChangesAsync();

        _logger.LogInformation("Link created successfully with shortUrl: {ShortUrl} and id: {Id}.", link.ShortUrl, link.Id);
        return LinkResponse.From(link);
    }
}
