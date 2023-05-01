using Library.LearningManagement.Database;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class AssignmentGroupService
    {
        private static AssignmentGroupService? _instance;

        public void Add(AssignmentGroup assignmentGroup)
        {
            FakeDatabase.AssignmentGroups.Add(assignmentGroup);
        }
        public void Remove(AssignmentGroup assignmentGroup)
        {
            FakeDatabase.AssignmentGroups.Remove(assignmentGroup);
        }
        public AssignmentGroup? GetById(int id)
        {
            return FakeDatabase.AssignmentGroups.FirstOrDefault(a => a.Id == id);
        }
        public IEnumerable<AssignmentGroup?> AssignmentGroups
        {
            get
            {
                return FakeDatabase.AssignmentGroups.Where(a => a is AssignmentGroup);
            }
        }
        private AssignmentGroupService()
        {

        }

        public static AssignmentGroupService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssignmentGroupService();
                }
                return _instance;
            }
        }

        public void AddAssignment(AssignmentGroup assignmentGroup, Assignment assignment)
        {
            foreach (var item in FakeDatabase.AssignmentGroups)
            {
                if (item.Name == assignmentGroup.Name)
                {
                    item.Assignments.Add(assignment);
                }
            }
        }
        public void RemoveAssignment(AssignmentGroup assignmentGroup, Assignment assignment)
        {
            foreach (var item in FakeDatabase.AssignmentGroups)
            {
                if (item.Name == assignmentGroup.Name)
                {
                    item.Assignments.Remove(assignment);
                }
            }
        }
    }
}
