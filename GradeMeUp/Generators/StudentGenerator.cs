using System;
using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;
using System.Data.SQLite;
using System.Diagnostics;
using GradeMeUp.Models;

namespace GradeMeUp.Generators
{
    public class StudentGenerator
    {
        public long RandomizerSeed { get; set; }
        public SQLiteConnection Connection { get; set; }

        public StudentGenerator(SQLiteConnection connection)
        {
            Connection = new SQLiteConnection(connection);
        }

        public void Generate(int numStudents = 1, int gender = 0)
        {
            var students = new List<Student>();
            for (var i = 0; i < numStudents; i++)
            {
                var student = new Faker<Student>()
                                .RuleFor(s => s.Gender, f => f.PickRandom<Gender>())
                                .RuleFor(s => s.FirstName, (f, s) => f.Name.FirstName((Name.Gender)s.Gender))
                                .RuleFor(s => s.LastName, (f, s) => f.Name.LastName((Name.Gender)s.Gender))
                                .FinishWith((f, s) => Trace.WriteLine("Student Created!"));
                students.Add(student.Generate());
            }

            foreach (var student in students)
            {
                var row = new Dictionary<string, object>
                {
                    { nameof(student.FirstName), student.FirstName },
                    { nameof(student.LastName), student.LastName },
                    { nameof(student.Gender), student.Gender }
                };

                var DB = new DBConnection(Connection);

                DB.Table("Students").Insert(row);
            }
        }
    }
}
