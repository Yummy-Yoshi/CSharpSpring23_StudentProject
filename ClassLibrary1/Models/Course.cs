namespace Library.LearningManagement.Models
{
    public class Course
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Person> Roster { get; set; }

        public List<Assignment> Assignments { get; set; }

        public List<AssignmentGroup> AssignmentGroups { get; set; }

        public List<Module> Modules { get; set; }

        public Course()
        {
            Code = string.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Roster = new List<Person>();
            Assignments = new List<Assignment>();
            AssignmentGroups = new List<AssignmentGroup>();
            Modules = new List<Module>();
        }

        public virtual string Display => $"{Code} - {Name}";

        public override string ToString()
        {
            return Display;
        }

    }
}