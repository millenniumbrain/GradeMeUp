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
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public long CourseID { get; set; }

        public List <Assignment> Assignments { get; set; }

        public Student()
        {
        }

        public static bool Insert(Dictionary<string, object> insertValues)
        {
            string insertString = "INSERT INTO students";
            string columns = "(";
            string values = "VALUES(";

            var i = 0;
            foreach (var entry in insertValues)
            {
                columns += $"{entry.Key},";
                if (entry.Value is string) 
                {
                    values += $"'{entry.Value}',";
                } else
                {
                    values += $"'{entry.Value}',";

                }
                i++;
                if (insertValues.Count() == i)
                {
                    columns = columns.Remove(columns.Length - 1);
                    columns += ")";
                    values = values.Remove(values.Length - 1);
                    values += ")";
                }               
            }

            insertString = $"{insertString}{columns}{values};";

            var connection = $"Data Source=courses.sqlite";

            using (var DB = new SQLiteConnection(connection))
            {
                DB.Open();
                var command = new SQLiteCommand(insertString, DB);

                command.ExecuteNonQuery();
            }

            return false;
        }
    }
}
