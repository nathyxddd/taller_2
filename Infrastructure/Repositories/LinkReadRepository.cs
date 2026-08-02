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

    public Task<LinkReadModel?> GetByIdAsync(long id)
        => _context.LinkReadModels.FirstOrDefaultAsync(l => l.Id == id);

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

    public async Task AddAsync(LinkReadModel readModel)
    {
        await _context.LinkReadModels.AddAsync(readModel);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(LinkReadModel readModel)
    {
        _context.LinkReadModels.Update(readModel);
        await _context.SaveChangesAsync();
    }
}
