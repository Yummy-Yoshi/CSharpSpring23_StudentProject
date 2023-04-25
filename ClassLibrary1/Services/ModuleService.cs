using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class ModuleService
    {
        private static ModuleService? _instance;

        public void Add(Module module)
        {
            FakeDatabase.Modules.Add(module);
        }

        public void Remove(Module module)
        {
            FakeDatabase.Modules.Remove(module);
        }

        public Module? GetById(int id)
        {
            return FakeDatabase.Modules.FirstOrDefault(m => m.Id == id);
        }

        public IEnumerable<Module?> Modules
        {
            get
            {
                return FakeDatabase.Modules.Where(m => m is Module);
            }
        }

        private ModuleService()
        {

        }

        public static ModuleService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ModuleService();
                }
                return _instance;
            }
        }
        public void AddContent(Module module, ContentItem contentItem)
        {
            foreach (var item in FakeDatabase.Modules)
            {
                if (item.Name == module.Name)
                {
                    item.Content.Add(contentItem);
                }
            }
        }

        public void RemoveContent(Module module, ContentItem contentItem)
        {
            foreach (var item in FakeDatabase.Modules)
            {
                if (item.Name == module.Name)
                {
                    item.Content.Remove(contentItem);
                }
            }
        }

    }
}
