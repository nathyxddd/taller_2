using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Shortly.Domain.Entities;

[Table("link_read_models")]
[Index(nameof(ShortUrl), IsUnique = true)]
public class LinkReadModel
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(20248)]
    public string OriginalUrl { get; set; } = null!;

    [Required]
    [MaxLength(32)]
    public string ShortUrl { get; set; } = null!;

    public int Clicks { get; set; }

    public long UserId { get; set; }

    public LinkReadModel()
    {
    }

    public LinkReadModel(string originalUrl, string shortUrl, long userId, int clicks = 0)
    {
        OriginalUrl = originalUrl;
        ShortUrl = shortUrl;
        UserId = userId;
        Clicks = clicks;
    }

    public LinkReadModel(long id, string originalUrl, string shortUrl, int clicks, long userId)
    {
        Id = id;
        OriginalUrl = originalUrl;
        ShortUrl = shortUrl;
        Clicks = clicks;
        UserId = userId;
    }
}
