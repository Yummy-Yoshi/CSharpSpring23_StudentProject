using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Announcement
    {
        public string Title { get; set; }
        public Person Poster { get; set; }
        public string Info { get; set; }

        public virtual string Display => $"{Title}:\nPosted by:{Poster}\n\t{Info}";

        public Announcement()
        {
            Title = string.Empty;
            Poster = new Person();
            Info = string.Empty;
        }

        public override string ToString()
        {
            return Display;
        }
    }
}