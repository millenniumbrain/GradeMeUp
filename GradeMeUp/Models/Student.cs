using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMeUp.Models
{
    public class Student
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public int Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Course> Courses { get; set; }

        public List<Course> GetCourses()
        {
            return null;
        }

        public static List<Student> All()
        {
            var connection = $"Data Source=courses.sqlite";
            string allStudents = "SELECT * FROM students";

            using (var DB = new SQLiteConnection(connection))
            {
                DB.Open();
                var students = new List<Student>();

                using (var command = new SQLiteCommand(allStudents, DB))
                {
                    var result = command.ExecuteReader();


                    while (result.Read())
                    {
                        var student = new Student();
                        student.ID = (long)result[nameof(student.ID)];
                        student.FirstName = result[nameof(student.FirstName)].ToString();
                        student.LastName = result[nameof(student.LastName)].ToString();

                        if (DateTime.TryParse(result[nameof(student.CreatedAt)].ToString(), out DateTime createdAt))
                        {
                            student.CreatedAt = createdAt;
                        }

                        if (DateTime.TryParse(result[nameof(student.UpdatedAt)].ToString(), out DateTime updatedAt))
                        {
                            student.UpdatedAt = updatedAt;
                        }

                        students.Add(student);
                    }
 
                }

                return students;
            }
        }
    }
}
