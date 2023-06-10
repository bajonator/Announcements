using Announcements.Core.Domains;
using Announcements.Core.Models;
using Announcements.Core.Models.Domains;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Announcements.Core.ViewModels
{
    public class AnnouncementsViewModel
    {
        public IEnumerable<Announcement> Announcements { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public FilterAnnouncements FilterAnnouncements { get; set; }
        
    }
}
