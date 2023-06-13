using Announcements.Core;
using Announcements.Core.Repositories;
using Announcements.Persistence.Repositories;

namespace Announcements.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context; 
            Announcement = new AnnouncementRepository(context);
        }
        public IAnnouncementRepository Announcement { get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
