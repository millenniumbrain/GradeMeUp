﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMeUp.Models
{
    public class CourseStudent
    {
        public long ID { get; set; }
        public long CourseID { get; set; }
        public long StudentID { get; set; }
    }
}
