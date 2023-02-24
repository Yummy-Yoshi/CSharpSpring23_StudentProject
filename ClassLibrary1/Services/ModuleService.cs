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
        private List<Module> moduleList = new List<Module>();

        public void Add(Module module)
        {
            moduleList.Add(module);
        }


        public List<Module> Modules
        {
            get
            {
                return moduleList;
            }
        }
    }
}
