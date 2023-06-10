using Announcements.Core.Domains;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Announcements.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Announcements = new Collection<Announcement>();
            Categories = new Collection<Category>();
        }

        public ICollection<Announcement> Announcements { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
