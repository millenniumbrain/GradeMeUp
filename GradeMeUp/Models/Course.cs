using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMeUp.Models
{
    public class Course
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; }
        public List<Assignment> Assignments { get; set; }

        public static List<Course> All()
        {
            var connection = $"Data Source=courses.sqlite";
            string allStudents = "SELECT * FROM Courses";

            using (var DB = new SQLiteConnection(connection))
            {
                DB.Open();
                var courses = new List<Course>();

                using (var command = new SQLiteCommand(allStudents, DB))
                {
                    var result = command.ExecuteReader();


                    while (result.Read())
                    {
                        var course = new Course();
                        course.ID = (long)result[nameof(course.ID)];
                        course.Name = result[nameof(course.Name)].ToString();
                        courses.Add(course);
                    }

                }

                return courses;
            }
        }
    }
}
