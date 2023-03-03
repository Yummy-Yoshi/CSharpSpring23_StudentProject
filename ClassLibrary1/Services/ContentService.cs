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
        private List<ContentItem> contentList;
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
            contentList = new List<ContentItem>();
        }

        public void Add(ContentItem content)
        {
            contentList.Add(content);
        }


        public List<ContentItem> Contents
        {
            get
            {
                return contentList;
            }
        }
    }
}
