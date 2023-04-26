using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class AssignmentService
    {
        private static AssignmentService? _instance;
        public IEnumerable<Assignment?> Assignments
        {
            get
            {
                return FakeDatabase.Assignments.Where(a => a is Assignment);
            }
        }
        private AssignmentService()
        {

        }

        public static AssignmentService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssignmentService();
                }
                return _instance;
            }
        }

        public void Add(Assignment assignment)
        {
            FakeDatabase.Assignments.Add(assignment);
        }
        public void Remove(Assignment assignment)
        {
            FakeDatabase.Assignments.Remove(assignment);
        }
        public Assignment? GetById(int id)
        {
            return FakeDatabase.Assignments.FirstOrDefault(a => a.Id == id);
        }
    }
}
