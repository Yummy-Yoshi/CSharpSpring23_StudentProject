using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.LearningManagement.ViewModels
{
    public class StudentViewViewModel : INotifyPropertyChanged
    {
        public StudentViewViewModel()
        {
            
        }
        public ObservableCollection<Person> People
        {
            get
            {
                var filteredList = StudentService
                    .Current
                    .Students
                    .Where(
                    s => s.Name.ToUpper().Contains(Query?.ToUpper() ?? string.Empty)).Where(s => s is Student);
                return new ObservableCollection<Person>(filteredList);
            }
        }
        public string Title { get => "Student Menu"; }

        public Person SelectedPerson { get; set; }

        private string query;
        public string Query
        {
            get => query;
            set
            {
                query = value;
                NotifyPropertyChanged(nameof(People));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void SeeCoursesClick(Shell s)
        {
            var idParam = SelectedPerson?.Id ?? 0;
            if(SelectedPerson != null)
            {
                s.GoToAsync($"//StudentCourseDetail?studentId={idParam}");
            } 
        }
    }
}
