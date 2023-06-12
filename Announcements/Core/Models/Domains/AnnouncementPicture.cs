using Announcements.Core.Domains;
using System.ComponentModel.DataAnnotations;

namespace Announcements.Core.Models.Domains
{
    public class AnnouncementPicture
    {
        public int Id { get; set; }
        public byte[] ImageData { get; set; }
        [Required]
        public int AnnouncementId { get; set; }
        public Announcement Announcement { get; set; }
    }
}
