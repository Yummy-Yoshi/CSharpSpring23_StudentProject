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
        private CourseService courseService;
        private ContentService contentService;

        public ModuleHelper()
        {
            contentService = ContentService.Current;
            courseService = CourseService.Current;
        }

        public Module CreateModuleRecord(Course c)
        {
            Console.WriteLine("Enter module name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter module description:");
            var description = Console.ReadLine() ?? string.Empty;

            var module = new Module
            {
                Name = name,
                Description = description,
                //Content = content,
            };

            Console.WriteLine("Create content for module? (y/n):");
            var response = Console.ReadLine() ?? string.Empty;

            while (response.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What type of content would you like to add?");
                Console.WriteLine("1. Assignment");
                Console.WriteLine("2. File");
                Console.WriteLine("3. Page");
                var contentChoice = int.Parse(Console.ReadLine() ?? "0");

                switch(contentChoice)
                {
                    case 1:
                        var newAssignmentContent = CreateAssignmentItem(c);
                        if(newAssignmentContent != null)
                        {
                            module.Content.Add(newAssignmentContent);
                        }
                        break;
                    case 2:
                        var newFileContent = CreateFileItem(c);
                        if (newFileContent != null)
                        {
                            module.Content.Add(newFileContent);
                        }
                        break;
                    case 3:
                        var newPageContent = CreatePageItem(c);
                        if (newPageContent != null)
                        {
                            module.Content.Add(newPageContent);
                        }
                        break;
                    default:
                        break;
                }

                //var newContent = contentHelper.CreateContentRecord();
                //contentService.Add(newContent);
                //content.Add(newContent);

                Console.WriteLine("Add more content items to module? (y/n)");
                response = Console.ReadLine() ?? string.Empty;
            }

            return module;
        }

        public AssignmentItem? CreateAssignmentItem(Course c)
        {
            Console.WriteLine("Enter assignment name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter assignment description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Which assignment should be added?");
            c.Assignments.ForEach(Console.WriteLine);
            var choice = int.Parse(Console.ReadLine() ?? "-1");

            if(choice >= 0)
            {
                var assignment = c.Assignments.FirstOrDefault(a => a.Id == choice);
                return new AssignmentItem
                {
                    Assignment = assignment,
                    Name = name,
                    Description = description
                };
            }
            return null;
        }
        public FileItem? CreateFileItem(Course c)
        {
            Console.WriteLine("Enter file name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter file description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter a path to the file:");
            var filepath = Console.ReadLine();

            return new FileItem { 
                Name = name,
                Description = description,
                Path= filepath
            };
        }

        public PageItem? CreatePageItem(Course c)
        {
            Console.WriteLine("Enter page name:");
            var name = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter page description:");
            var description = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Enter page content:");
            var body = Console.ReadLine();

            return new PageItem
            {
                Name = name,
                Description = description,
                HtmlBody = body
            };
        }
    }
}
