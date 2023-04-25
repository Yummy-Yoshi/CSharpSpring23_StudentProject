using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class ContentService
    {
        private static ContentService? _instance;

        public static ContentService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContentService();
                }

                return _instance;
            }
        }

        private ContentService()
        {

        }

        public void Add(ContentItem content)
        {
            FakeDatabase.ContentItems.Add(content);
        }
        public void Remove(ContentItem content)
        {
            FakeDatabase.ContentItems.Remove(content);
        }
        public ContentItem? GetById(int id)
        {
            return FakeDatabase.ContentItems.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<ContentItem?> ContentItems
        {
            get
            {
                return FakeDatabase.ContentItems.Where(c => c is ContentItem);
            }
        }
    }
}
