using Library.LearningManagement.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.LearningManagement.ViewModels
{
    public class PersonViewModel
    {
        public PersonDTO Dto { get; set; }

        public string Display
        {
            get
            {
                return $"[{Dto.Id}] {Dto.Name}";
            }
        }

        public PersonViewModel() { }

        public PersonViewModel(PersonDTO dto)
        {
            Dto = dto;
        }
    }
}
