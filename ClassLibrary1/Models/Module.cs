using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Module
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
        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<ContentItem> Content { get; set; }

        public Module()
        {
            Content = new List<ContentItem>();
        }

        public virtual string Display => $"[{Id}] {Name}: {Description}\n" +
            $"\n{Name} Content:\n{string.Join("\n", Content.Select(a => a.ToString()).ToArray())}";

        public override string ToString()
        {
            return Display;
        }

    }
}
