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
        private List<ContentItem> contentList = new List<ContentItem>();

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
