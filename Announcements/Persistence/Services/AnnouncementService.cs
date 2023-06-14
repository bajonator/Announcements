using Announcements.Core.Domains;
using Announcements.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using Announcements.Core.Service;
using Announcements.Core;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Announcements.Persistence.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnnouncementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Announcement> Get(string userId, int categoryId = 0, string title = null, IEnumerable<AnnouncementPicture> pictures = null)
        {
            return _unitOfWork.Announcement.Get(userId, categoryId, title, pictures);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Announcement.GetCategories();
        }

        public Announcement Get(int id, string userId)
        {
            return _unitOfWork.Announcement.Get(id, userId);
        }

        public void Update(Announcement announcement)
        {
            _unitOfWork.Announcement.Update(announcement);
            _unitOfWork.Complete();
        }

        public void Add(Announcement announcement)
        {
            _unitOfWork.Announcement.Add(announcement);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Announcement.Delete(id, userId);
            _unitOfWork.Complete();
        }

        public Announcement GetPreview(int id)
        {
            return _unitOfWork.Announcement.GetPreview(id);
        }

        public IEnumerable<AnnouncementPicture> GetPictures(int id)
        {
            return _unitOfWork.Announcement.GetPictures(id);
        }

        public void AddPhoto(Announcement announcement, List<IFormFile> files)
        {

            if (files != null && files.Count > 0)
            {
                announcement.Pictures = new List<AnnouncementPicture>();

                foreach (var photo in files)
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
    }
}
