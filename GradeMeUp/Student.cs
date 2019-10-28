using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMeUp
{
    public class Student
    {
        public string Name { get; set; }
        public int OverallScore { get; set; }

        public Student(string name, int overallScore)
        {
            Name = name;
            OverallScore = overallScore;
        }
    }
}
