namespace Library.LearningManagement.Models
{
    public class Course
    {
        public int CreditHours { get; set; }
        public string Code
        {
            get
            {
                return $"{Prefix}{Id}";
            }
        }

        private static int lastId = 0;

        public string? Prefix { get; set; }
        public int Id
        {
            get; private set;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Person> Roster { get; set; }

        public List<Assignment> Assignments { get; set; }

        public List<AssignmentGroup> AssignmentGroups { get; set; }

        public List<Module> Modules { get; set; }

        public List<Announcement> Announcements { get; set; }

        public Course()
        {
            CreditHours = 0;
            Name = string.Empty;
            Description = string.Empty;
            Roster = new List<Person>();
            Assignments = new List<Assignment>();
            AssignmentGroups = new List<AssignmentGroup>();
            Modules = new List<Module>();
            Announcements = new List<Announcement>();
            Id = ++lastId;
        }

        public virtual string Display => $"{Code} - {Name}";

        public override string ToString()
        {
            return Display;
        }

    }
}