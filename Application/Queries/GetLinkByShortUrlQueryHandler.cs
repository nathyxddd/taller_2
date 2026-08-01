using Shortly.Application.DTOs;
using Shortly.Application.Interfaces;

namespace Shortly.Application.Queries;

public sealed class GetLinkByShortUrlQueryHandler
{
    private readonly ILinkReadRepository _readRepository;

    public GetLinkByShortUrlQueryHandler(ILinkReadRepository readRepository)
    {
        _readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
    }

    public async Task<LinkResponse?> HandleAsync(GetLinkByShortUrlQuery query)
    {
        var readModel = await _readRepository.GetByShortUrlAsync(query.ShortUrl);
        return readModel == null ? null : LinkResponse.From(readModel);
    }
}
