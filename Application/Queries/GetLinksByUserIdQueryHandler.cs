using Shortly.Application.DTOs;
using Shortly.Application.Interfaces;

namespace Shortly.Application.Queries;

public sealed class GetLinksByUserIdQueryHandler
{
    private readonly ILinkReadRepository _readRepository;

    public GetLinksByUserIdQueryHandler(ILinkReadRepository readRepository)
    {
        _readRepository = readRepository ?? throw new ArgumentNullException(nameof(readRepository));
    }

    public async Task<List<LinkResponse>> HandleAsync(GetLinksByUserIdQuery query)
    {
        var readModels = await _readRepository.GetByUserIdAsync(query.UserId);
        return readModels.Select(LinkResponse.From).ToList();
    }
}
