using Shortly.Application.DTOs;
using Shortly.Application.Interfaces;

namespace Shortly.Application.Queries;

public sealed class GetAllLinksQueryHandler
{
    private readonly ILinkReadRepository _readRepository;

    public GetAllLinksQueryHandler(ILinkReadRepository readRepository)
    {
        _readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
    }

    public async Task<List<LinkResponse>> HandleAsync(GetAllLinksQuery query)
    {
        var readModels = await _readRepository.GetAllAsync();
        return readModels.Select(LinkResponse.From).ToList();
    }
}
