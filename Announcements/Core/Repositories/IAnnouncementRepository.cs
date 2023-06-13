using Announcements.Core.Domains;
using Announcements.Core.Models.Domains;
using System.Collections.Generic;

namespace Announcements.Core.Repositories
{
    public interface IAnnouncementRepository
    {
        IEnumerable<Announcement> Get(string userId, int categoryId = 0, string title = null, IEnumerable<AnnouncementPicture> pictures = null);
        IEnumerable<Category> GetCategories();
        Announcement Get(int id, string userId);
        void Update(Announcement announcement);
        void Add(Announcement announcement);
        void Delete(int id, string userId);
        Announcement GetPreview(int id);
        IEnumerable<AnnouncementPicture> GetPictures(int id);
    }
}
