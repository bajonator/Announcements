using Announcements.Core.Domains;
using Announcements.Core.Models;
using Announcements.Core.Models.Domains;
using Announcements.Core.Service;
using Announcements.Core.ViewModels;
using Announcements.Persistence;
using Announcements.Persistence.Extensions;
using Announcements.Persistence.Repositories;
using Announcements.Persistence.Services;
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
        private IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }
        public IActionResult Announcements()
        {
            var userId = User.GetUserId();

            var vm = new AnnouncementsViewModel
            {
                FilterAnnouncements = new FilterAnnouncements(),
                Announcements = _announcementService.Get(userId),
                Categories = _announcementService.GetCategories(),
            };

            return View(vm);
        }

        public IActionResult AnnouncementPreview(int id)
        {
            var announcement = _announcementService.GetPreview(id);

            var vm = new AnnouncementViewModel
            {
                Announcement = announcement,
                Categories = _announcementService.GetCategories(),
                Pictures = _announcementService.GetPictures(id)
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Announcements(AnnouncementsViewModel viewModel)
        {
            var userId = User.GetUserId();

            var announcements = _announcementService.Get(userId,
                viewModel.FilterAnnouncements.CategoryId,
                viewModel.FilterAnnouncements.Title,
                viewModel.Pictures);

            return PartialView("_AnnouncementsTable", announcements);
        }

        public IActionResult Announcement(int id = 0)
        {
            var userId = User.GetUserId();

            var announcement = id == 0 ?
                new Announcement { Id = 0, AddDate = DateTime.Today } : _announcementService.Get(id, userId);

            var vm = new AnnouncementViewModel
            {
                Announcement = announcement,
                Heading = id == 0 ?
                "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia",
                Categories = _announcementService.GetCategories(),
                Pictures= _announcementService.GetPictures(id)                
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Announcement(Announcement announcement, List<IFormFile> files)
        {
            var userId = User.GetUserId();
            announcement.UserId = userId;

            _announcementService.AddPhoto(announcement, files);

            if (!ModelState.IsValid)
            {
                var vm = new AnnouncementViewModel
                {
                    Announcement = announcement,
                    Heading = announcement.Id == 0 ?
                    "Dodawanie nowego ogłoszenia" : "Edytowanie ogłoszenia",
                    Categories = _announcementService.GetCategories(),
                    Pictures = _announcementService.GetPictures(announcement.Id)
                };

                return View("Announcement", vm);
            }

            if (announcement.Id == 0)
                _announcementService.Add(announcement);
            else
                _announcementService.Update(announcement);


            return RedirectToAction("Announcements");
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _announcementService.Delete(id, userId);
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
