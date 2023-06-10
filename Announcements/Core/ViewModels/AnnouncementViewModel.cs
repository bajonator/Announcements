using Announcements.Core.Domains;
using Announcements.Core.Models.Domains;
using System.Collections;
using System.Collections.Generic;

namespace Announcements.Core.ViewModels
{
    public class AnnouncementViewModel
    {
        public string Heading { get; set; }
        public Announcement Announcement { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
