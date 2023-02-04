﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Assignment
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TotalAvailablePoints { get; set; }

        public string DueDate { get; set; }
    }
}
