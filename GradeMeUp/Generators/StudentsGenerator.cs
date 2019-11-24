using System;
using System.Collections.Generic;
using Bogus;
using Bogus.DataSets;
using System.Data.SQLite;
using System.Diagnostics;
using GradeMeUp.Models;

namespace GradeMeUp.Generators
{
    public class StudentsGenerator
    {

        public static bool Generate(int numStudents = 1, int gender = 0, string dbConnection = "courses.sqlite")
        {
            Randomizer.Seed = new Random(8675309);

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

            var connection = $"Data Source={dbConnection}";
            foreach (var student in students)
            {
                var row = new Dictionary<string, object>
                {
                    { nameof(student.FirstName), student.FirstName },
                    { nameof(student.LastName), student.LastName },
                    { nameof(student.Gender), student.Gender }
                };

                Student.Insert(row);
            }
            return false;
        }
    }
}
