using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.LearningManagement.Helpers
{
    public class ContentHelper
    {
        public ContentItem CreateContentRecord()
        {
            Console.WriteLine("Enter content item name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter content item description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter content item path:");
            var path = Console.ReadLine() ?? string.Empty;

            var contentItem = new ContentItem
            {
                Name = name,
                Description = description,
                Path = path,
            };

            return contentItem;
        }
    }
}
