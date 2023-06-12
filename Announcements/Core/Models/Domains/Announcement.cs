using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using Announcements.Core.Models.Domains;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;

namespace Announcements.Core.Domains
{
    public class Announcement
    {
        public Announcement()
        {
            Pictures = new Collection<AnnouncementPicture>();
        }
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

        [Display(Name = "Cena")]
        public decimal? Price { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime? AddDate { get; set; }

        public string UserId { get; set; }

        public ICollection<AnnouncementPicture> Pictures { get; set; }
        public Category Category { get; set; }
        public ApplicationUser User { get; set; }

    }
}
