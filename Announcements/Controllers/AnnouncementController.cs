using Announcements.Core.Domains;
using Announcements.Core.Models;
using Announcements.Core.Models.Domains;
using Announcements.Core.ViewModels;
using Announcements.Persistence;
using Announcements.Persistence.Extensions;
using Announcements.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Announcements.Controllers
{
    public class AnnouncementController : Controller
    {
        private AnnouncementRepository _announcementRepository;
        public AnnouncementController(ApplicationDbContext context)
        {
            _announcementRepository = new AnnouncementRepository(context);
        }
        public IActionResult Announcements()
        {
            var userId = User.GetUserId();

            var vm = new AnnouncementsViewModel
            {
                FilterAnnouncements = new FilterAnnouncements(),
                Announcements = _announcementRepository.Get(userId),
                Categories = _announcementRepository.GetCategories()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Announcements(AnnouncementsViewModel viewModel)
        {
            var userId = User.GetUserId();

            var announcements = _announcementRepository.Get(userId,
                viewModel.FilterAnnouncements.CategoryId,
                viewModel.FilterAnnouncements.Title);

            return PartialView("_AnnouncementsTable", announcements);
        }

        public IActionResult Announcement(int id = 0)
        {
            var userId = User.GetUserId();

            var announcement = id == 0 ?
                new Announcement { Id = 0, AddDate = DateTime.Today } : _announcementRepository.Get(id, userId);

            var vm = new AnnouncementViewModel
            {
                Announcement = announcement,
                Heading = id == 0 ?
                "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia",
                Categories = _announcementRepository.GetCategories()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Announcement(Announcement announcement)
        {
            var userId = User.GetUserId();
            announcement.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new AnnouncementViewModel
                {
                    Announcement = announcement,
                    Heading = announcement.Id == 0 ?
                    "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia",
                    Categories = _announcementRepository.GetCategories()
                };

                return View("Announcement", vm);
            }

            if (announcement.Id == 0)
                _announcementRepository.Add(announcement);
            else
                _announcementRepository.Update(announcement);

            return RedirectToAction("Announcements");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _announcementRepository.Delete(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { success = false, message = ex.Message });
            }

            return Json(new { success = true });
        }
    }
}
