using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class AnnouncementService
    {
        private static AnnouncementService? _instance;

        public IEnumerable<Announcement?> Announcements
        {
            get
            {
                return FakeDatabase.Announcements.Where(a => a is Announcement);
            }
        }

        private AnnouncementService()
        {

        }

        public static AnnouncementService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AnnouncementService();
                }
                return _instance;
            }
        }

        public void Add(Announcement announcement)
        {
            FakeDatabase.Announcements.Add(announcement);
        }

        public void Remove(Announcement announcement)
        {
            FakeDatabase.Announcements.Remove(announcement);
        }

        public Announcement? GetById(int id)
        {
            return FakeDatabase.Announcements.FirstOrDefault(a => a.Id == id);
        }
    }
}
