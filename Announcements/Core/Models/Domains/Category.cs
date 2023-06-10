using Announcements.Core.Domains;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Announcements.Core.Models.Domains
{
    public class Category
    {
        public Category()
        {
            Announcements = new Collection<Announcement>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //public string UserId { get; set; }

        public ICollection<Announcement> Announcements { get; set; }
        //public ApplicationUser User { get; set; }
    }
}
