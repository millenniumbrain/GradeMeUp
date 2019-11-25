using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions;
using GradeMeUp.Models;

namespace GradeMeUp.Generators
{
    public class AssignmentGenerator
    {
        public long RandomizerSeed { get; set; }
        public SQLiteConnection Connection { get; set; }

        public AssignmentGenerator(SQLiteConnection connection)
        {
            Connection = new SQLiteConnection(connection);
        }

        public void Generate(int numAssignments = 1, int assignmentType = 1)
        {
            var assignments = new List<Assignment>();

            for (var i = 0; i < numAssignments; i++)
            {
                var assignment = new Faker<Assignment>()
                                    .RuleFor(a => a.Name, f => f.Lorem.Word())
                                    .RuleFor(a => a.Grade, f => f.Random.Int(0, 100).OrNull(f, .5f))
                                    .RuleFor(a => a.PointsPossible, f => f.Random.Int(5, 100))
                                    .RuleFor(a => a.AssignmentType, f => f.PickRandom<AssignmentType>())
                                    .RuleFor(a => a.StudentID, f => f.Random.Long(1, 10).OrNull(f, .5f))
                                    .RuleFor(a => a.CourseID, f => f.Random.Long(1, 10).OrNull(f, .5f))
                                    .FinishWith((f, s) => Trace.WriteLine("Assignment Created!"));
                assignments.Add(assignment.Generate());
            }

            foreach (var assignment in assignments)
            {
                var row = new Dictionary<string, object>
                {
                    { nameof(assignment.Name), assignment.Name },
                    { nameof(assignment.Grade), assignment.Grade },
                    { nameof(assignment.PointsPossible), assignment.PointsPossible },
                    { nameof(assignment.AssignmentType), assignment.AssignmentType },
                    { nameof(assignment.StudentID), assignment.StudentID },
                    { nameof(assignment.CourseID), assignment.CourseID }
                };

                var DB = new DBConnection(Connection);

                DB.Table("Assignments").Insert(row);
            }
        }
    }
}
