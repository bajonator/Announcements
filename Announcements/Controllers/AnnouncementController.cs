using Announcements.Core.Domains;
using Announcements.Core.Models;
using Announcements.Core.Models.Domains;
using Announcements.Core.ViewModels;
using Announcements.Persistence;
using Announcements.Persistence.Extensions;
using Announcements.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
                Categories = _announcementRepository.GetCategories(),
            };

            return View(vm);
        }

        public IActionResult AnnouncementPreview(int id)
        {
            var announcement = _announcementRepository.GetPreview(id);

            var vm = new AnnouncementViewModel
            {
                Announcement = announcement,
                Categories = _announcementRepository.GetCategories(),
                Pictures = _announcementRepository.GetPictures(id)
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Announcements(AnnouncementsViewModel viewModel)
        {
            var userId = User.GetUserId();

            var announcements = _announcementRepository.Get(userId,
                viewModel.FilterAnnouncements.CategoryId,
                viewModel.FilterAnnouncements.Title,
                viewModel.Pictures);

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
                Categories = _announcementRepository.GetCategories(),
                Pictures= _announcementRepository.GetPictures(id)                
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Announcement(Announcement announcement, List<IFormFile> files)
        {
            var userId = User.GetUserId();
            announcement.UserId = userId;

            AddPhoto(announcement);

            if (!ModelState.IsValid)
            {
                var vm = new AnnouncementViewModel
                {
                    Announcement = announcement,
                    Heading = announcement.Id == 0 ?
                    "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia",
                    Categories = _announcementRepository.GetCategories(),
                    Pictures = _announcementRepository.GetPictures(announcement.Id)
                };

                return View("Announcement", vm);
            }

            if (announcement.Id == 0)
                _announcementRepository.Add(announcement);
            else
                _announcementRepository.Update(announcement);

            return RedirectToAction("Announcements");
        }

        private void AddPhoto(Announcement announcement)
        {
            var photos = Request.Form.Files;

            if (photos != null && photos.Count > 0)
            {
                announcement.Pictures = new List<AnnouncementPicture>();

                foreach (var photo in photos)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        photo.CopyTo(memoryStream);
                        var picture = new AnnouncementPicture
                        {
                            ImageData = memoryStream.ToArray(),
                        };
                        announcement.Pictures.Add(picture);
                    }
                }
            }
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
