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
    public class CourseGenerator
    {
        public long RandomizerSeed { get; set; }
        public SQLiteConnection Connection { get; set; }

        public CourseGenerator(SQLiteConnection connection)
        {
            Connection = new SQLiteConnection(connection);
        }

        public void Generate(int numCourses)
        {
            var courses = new List<Course>();
            for (var i = 0; i < numCourses; i++)
            {
                var course = new Faker<Course>()
                                .RuleFor(c => c.Name, f => f.Lorem.Word())
                                .FinishWith((f, s) => Trace.WriteLine("Course Created!"));
                courses.Add(course.Generate());
            }

            foreach (var course in courses)
            {
                var row = new Dictionary<string, object>()
                {
                    { nameof(course.Name), course.Name }
                };

                var DB = new DBConnection(Connection);

                DB.Table("Courses").Insert(row);
            }
        }
    }
}
