using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Database
{
    public static class FakeDatabase
    {
        private static List<Person> people= new List<Person>();
        private static List<Course> courses= new List<Course>();
        private static List<Announcement> announcements= new List<Announcement>();
        private static List<Module> modules = new List<Module>();
        private static List<ContentItem> contentItems = new List<ContentItem>();
        public static List<Person> People
        {
            get
            {
                return people;
            }
        }

        public static List<Course> Courses
        {
            get
            {
                return courses;
            }
        }

        public static List<Announcement> Announcements 
        {
            get
            {
                return announcements;
            }
        }
        public static List<Module> Modules
        {
            get
            {
                return modules;
            }
        }
        public static List<ContentItem> ContentItems
        {
            get
            {
                return contentItems;
            }
        }
    }
}
