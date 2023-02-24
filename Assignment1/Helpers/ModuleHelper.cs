using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.LearningManagement.Helpers
{
    public class ModuleHelper
    {
        private ModuleService moduleService = new ModuleService();
        private ContentService contentService = new ContentService();

        public Module CreateModuleRecord()
        {
            var contentHelper = Program.contentHelper;

            Console.WriteLine("Enter module name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter module description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Create content for module? (y/n):");
            var response = Console.ReadLine() ?? string.Empty;
            var content = new List<ContentItem>();

            while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                var newContent = contentHelper.CreateContentRecord();
                contentService.Add(newContent);
                content.Add(newContent);

                Console.WriteLine("Add more content items to module? (y/n)");
                response = Console.ReadLine() ?? string.Empty;
            }

            var module = new Module
            {
                Name = name,
                Description = description,
                Content = content,
            };

            return module;
        }
    }
}
