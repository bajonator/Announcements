using Announcements.Core.Repositories;
using Announcements.Persistence.Repositories;

namespace Announcements.Core
{
    public interface IUnitOfWork
    {
        IAnnouncementRepository Announcement { get; }
        void Complete();
    }
}
