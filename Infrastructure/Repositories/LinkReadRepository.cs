using Microsoft.EntityFrameworkCore;
using Shortly.Application.Interfaces;
using Shortly.Domain.Entities;
using Shortly.Infrastructure.Persistence;

namespace Shortly.Infrastructure.Repositories;

public sealed class LinkReadRepository : ILinkReadRepository
{
    private readonly AppDbContext _context;

    public LinkReadRepository(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<LinkReadModel?> GetByShortUrlAsync(string shortUrl)
        => _context.LinkReadModels
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.ShortUrl == shortUrl);

    public Task<List<LinkReadModel>> GetAllAsync()
        => _context.LinkReadModels
            .AsNoTracking()
            .ToListAsync();

    public Task<List<LinkReadModel>> GetByUserIdAsync(long userId)
        => _context.LinkReadModels
            .AsNoTracking()
            .Where(l => l.UserId == userId)
            .ToListAsync();
}
