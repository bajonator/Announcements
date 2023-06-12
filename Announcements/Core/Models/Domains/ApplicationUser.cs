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
        }

        public ICollection<Announcement> Announcements { get; set; }
    }
}
