using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMeUp
{
    public class Assignment
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int PointsPossible { get; set; }
        public long StudentID { get; set; }
        public long AssignmentTypeID { get; set; }

        public Assignment(string name, int points, int pointsPossible)
        {
            Name = name;
            Points = points;
            PointsPossible = pointsPossible;
        }
    }
}
