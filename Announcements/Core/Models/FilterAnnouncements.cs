using System.ComponentModel.DataAnnotations;

namespace Announcements.Core.Models
{
    public class FilterAnnouncements
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
}
