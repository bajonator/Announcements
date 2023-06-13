using Announcements.Core.Domains;
using Announcements.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Announcements.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Announcement> Announcements { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<AnnouncementPicture> Pictures { get; set; }
        int SaveChanges();
    }
}
