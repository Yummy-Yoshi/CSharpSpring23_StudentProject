using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.LearningManagement.Models
{
    public class Announcement
    {
        private static int lastId = 0;
        private int id = 0;
        public int Id
        {
            get
            {
                if (id == 0)
                {
                    id = ++lastId;
                }
                return id;
            }
        }

        public string Title { get; set; }
        public Person Poster { get; set; }
        public string Info { get; set; }

        //public virtual string Display => $"{Id}. {Title}:\nPosted by: {Poster?.Name}\n\t{Info}";

        public Announcement()
        {
            Title = string.Empty;
            Poster = new Person();
            Info = string.Empty;
        }

        public override string ToString()
        {
            return $"[{Id}] {Title}";
        }
    }
}