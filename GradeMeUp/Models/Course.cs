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
        public long Grade { get; set; }
        public long TotalPoints { get; set; }
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



        public List<Assignment> GetAssignments()
        {
            if (ID != null)
            {
                var connection = $"Data Source=courses.sqlite";
                string allStudents = $"SELECT Assignments.ID, Assignments.Name, Assignments.AssignmentType Assignments.Grade, Assignments.PointsPossible FROM Courses INNER JOIN Assignment ON Courses.ID = Assignments.CourseID WHERE CourseID = {ID}; ";

                using (var DB = new SQLiteConnection(connection))
                {
                    DB.Open();
                    var assignments = new List<Assignment>();

                    using (var command = new SQLiteCommand(allStudents, DB))
                    {
                        var result = command.ExecuteReader();


                        while (result.Read())
                        {
                            var assignment = new Assignment();
                            assignment.ID = (long)result[nameof(assignment.ID)];
                            assignment.Name = result[nameof(assignment.Name)].ToString();
                            assignment.AssignmentType = (int)result[nameof(assignment.AssignmentType)];
                            assignment.Grade = (int)result[nameof(assignment.Grade)];
                            assignment.PointsPossible = (int)result[nameof(assignment.PointsPossible)];
                            assignments.Add(assignment);
                        }

                    }

                    return assignments;
                }
            }
            else
            {
                return null;
            }
        }

        public void CalculateGrades()
        {
            var connection = $"Data Source=courses.sqlite";
            string allStudents = $"SELECT SUM(Assignments.Grade) AS Grade, SUM(Assignments.PointsPossible) AS PointsPossible FROM Courses INNER JOIN Assignments ON Courses.ID = Assignments.CourseID WHERE CourseID = {ID};";

            using (var DB = new SQLiteConnection(connection))
            {
                DB.Open();

                using (var command = new SQLiteCommand(allStudents, DB))
                {
                    var result = command.ExecuteReader();


                    while (result.Read())
                    {
                        var grade = result[nameof(Assignment.Grade)];
                        var totalPoints = result[nameof(Assignment.PointsPossible)];

                        if (grade == DBNull.Value)
                        {
                            Grade = 0;
                        }
                        else
                        {
                            Grade = (long)grade;
                        }


                        if (totalPoints == DBNull.Value)
                        {
                            TotalPoints = 0;
                        } else
                        {
                            TotalPoints = (long)totalPoints;
                        }

                    }

                }
            }
        }
    }
}
