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
        public List<Course> Courses { 
            get 
            {
                return GetCourses();
            } 
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

        private List<Course> GetCourses()
        {
            var connection = $"Data Source=courses.sqlite";
            string associatedCourse = $"SELECT CourseID FROM CoursesStudents WHERE StudentID = {ID};";
            var courses = new List<Course>();
            using (var DB = new SQLiteConnection(connection))
            {
                DB.Open();
                using (var command = new SQLiteCommand(associatedCourse, DB))
                {
                    var result = command.ExecuteReader();
                    try
                    {
                        while (result.Read())
                        {
                            long courseID = 0L;
                            if (result["CourseID"] == DBNull.Value)
                            {
                                courseID = 0L;
                            }
                            else
                            {
                                courseID = (long)result["CourseID"];
                            }
                            var course = Course.Find(courseID);
                            courses.Add(course);
                        }
                    } 
                    catch (IndexOutOfRangeException ex)
                    {
                        return new List<Course>();
                    }
                }
               return courses;
            }
        }
    }
}
