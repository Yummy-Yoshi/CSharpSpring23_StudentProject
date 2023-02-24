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
        public List<Assignment> assignmentList;

        private static AssignmentService? _instance;

        private AssignmentService()
        {
            assignmentList = new List<Assignment>();
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
            assignmentList.Add(assignment);
        }

        public List<Assignment> Assignments
        {
            get
            {
                return assignmentList;
            }
        }
    }
}
