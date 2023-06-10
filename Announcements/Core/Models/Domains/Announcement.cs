using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Announcements.Core.Models.Domains;

namespace Announcements.Core.Domains
{
    public class Announcement
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Pole Tytuł jest wymagane.")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "Pole Opis jest wymagane.")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Pole Kategoria jest wymagane.")]
        [Display(Name = "Kategoria")]
        public int CategoryId { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime? AddDate { get; set; }



        public string UserId { get; set; }

        public Category Category { get; set; }
        public ApplicationUser User { get; set; }

    }
}
