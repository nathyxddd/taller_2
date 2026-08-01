using Shortly.Domain.Entities;

namespace Shortly.Application.Interfaces;

public interface ILinkReadRepository
{
    Task<LinkReadModel?> GetByShortUrlAsync(string shortUrl);
    Task<List<LinkReadModel>> GetAllAsync();
    Task<List<LinkReadModel>> GetByUserIdAsync(long userId);
}
