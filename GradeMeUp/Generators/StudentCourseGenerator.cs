using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using GradeMeUp.Models;

namespace GradeMeUp.Generators
{
    public class CourseStudentGenerator
    {
        public long RandomizerSeed { get; set; }
        public SQLiteConnection Connection { get; set; }

        public CourseStudentGenerator(SQLiteConnection connection)
        {
            Connection = new SQLiteConnection(connection);
        }

        public bool Generate(int numEntries)
        {
            var entries = new List<CourseStudent>();
            for (var  i = 0; i < numEntries; i++)
            {
                var courseStudent = new Faker<CourseStudent>()
                                        .RuleFor(cs => cs.CourseID, f => f.Random.Long(1, numEntries))
                                        .RuleFor(cs => cs.StudentID, f => f.Random.Long(1, numEntries))
                                        .FinishWith((f, s) => Trace.WriteLine("Course Created!"));
                entries.Add(courseStudent.Generate());
            }

            foreach (var entry in entries)
            {
                var row = new Dictionary<string, object>()
                {
                    { nameof(entry.CourseID), entry.CourseID },
                    { nameof(entry.StudentID), entry.StudentID }
                };

                var DB = new DBConnection(Connection);
                DB.Table("CoursesStudents").Insert(row);
            }
            return false;
        }
    }
}
