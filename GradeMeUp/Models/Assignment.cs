using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMeUp
{
    public class Assignment
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int? Grade { get; set; }
        public int PointsPossible { get; set; }
        public AssignmentType AssignmentType { get; set; }
        public long? StudentID { get; set; }
        public long? CourseID { get; set; }

        public static List<Assignment> All()
        {
            var connection = $"Data Source=courses.sqlite";
            string allStudents = "SELECT * FROM Assignments";

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
                        if (result[nameof(assignment.Grade)] == DBNull.Value)
                        {
                            assignment.Grade = (int)result[nameof(assignment.Grade)];
                        }

                        if (result[nameof(assignment.PointsPossible)] == DBNull.Value)
                        {
                            assignment.PointsPossible = (int)result[nameof(assignment.PointsPossible)];
                        }

                        assignment.AssignmentType = (AssignmentType)result[nameof(assignment.AssignmentType)];

                        assignments.Add(assignment);
                    }

                }

                return assignments;
            }
        }
    }
}
