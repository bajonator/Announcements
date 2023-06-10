using Announcements.Core.Domains;
using Announcements.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Announcements.Persistence.Repositories
{
    public class AnnouncementRepository
    {
        private ApplicationDbContext _context;

        public AnnouncementRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public IEnumerable<Announcement> Get(string userId , int categoryId = 0, string title = null)
        {
            var announcement = _context.Announcements.Include(x => x.Category).AsQueryable();

            if (categoryId != 0)
                announcement = announcement.Where(x => x.CategoryId == categoryId);

            if (!string.IsNullOrWhiteSpace(title))
                announcement = announcement.Where(x => x.Title.Contains(title));

            return announcement.OrderBy(x => x.AddDate).ToList();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.OrderBy(x => x.Name).ToList();
        }

        public Announcement Get(int id, string userId)
        {
            var announcement = _context.Announcements.FirstOrDefault(x => x.Id == id && x.UserId == userId);

            return announcement;
        }

        public void Update(Announcement announcement)
        {
            var announcementToUpdate = _context.Announcements.Single(x => x.Id == announcement.Id);

            announcementToUpdate.CategoryId = announcement.CategoryId;
            announcementToUpdate.Description = announcement.Description;
            announcementToUpdate.Title = announcement.Title;
            
            _context.SaveChanges();
        }

        public void Add(Announcement announcement)
        {
            announcement.AddDate = DateTime.Now;
            _context.Announcements.Add(announcement);
            _context.SaveChanges();
        }

        public void Delete(int id, string userId)
        {
            var announcementToDelete = _context.Announcements
                .Single(x => x.Id == id && x.UserId == userId);

            _context.Announcements.Remove(announcementToDelete);
        }
    }
}
