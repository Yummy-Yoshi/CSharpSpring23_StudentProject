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
        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<ContentItem> Content { get; set; }

        public Module()
        {
            Content = new List<ContentItem>();
        }

        public virtual string Display => $"{Name}: {Description}\n" +
            $"\n{Name} Content:\n{string.Join("\n", Content.Select(a => a.ToString()).ToArray())}";

        public override string ToString()
        {
            return Display;
        }

    }
}
