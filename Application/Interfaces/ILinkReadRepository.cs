using Shortly.Domain.Entities;

namespace Shortly.Application.Interfaces;

public interface ILinkReadRepository
{
    Task<LinkReadModel?> GetByIdAsync(long id);
    Task<LinkReadModel?> GetByShortUrlAsync(string shortUrl);
    Task<List<LinkReadModel>> GetAllAsync();
    Task<List<LinkReadModel>> GetByUserIdAsync(long userId);
    Task AddAsync(LinkReadModel readModel);
    Task UpdateAsync(LinkReadModel readModel);
}
