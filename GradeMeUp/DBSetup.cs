using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Data.SQLite;

namespace GradeMeUp
{
    public class DBSetup
    {
        public static bool MigrateDB(string filePath = "courses.sqlite")
        {
            var connection = $"Data Source={filePath}";
            var migrationScripts = new List<string>() {
                File.ReadAllText("../../Scripts/courses.sql"),
                File.ReadAllText("../../Scripts/students.sql"),
                File.ReadAllText("../../Scripts/assignments.sql"),
                File.ReadAllText("../../Scripts/courses_students.sql")
            };

            if (!File.Exists(filePath))
            {
                Trace.WriteLine("Creating DB file....");
                SQLiteConnection.CreateFile(filePath);

                using (var DB = new SQLiteConnection(connection))
                {
                    DB.Open();
                    foreach(var script in migrationScripts)
                    {
                        using (var command = new SQLiteCommand(script, DB))
                        {
                            var rows = command.ExecuteNonQuery();
                            Trace.WriteLine($"Row(s) affected: {rows}");
                        }


                    }
                }
                
            } 
            else
            {
                Trace.WriteLine("Creating DB file....");
                SQLiteConnection.CreateFile(filePath);

                using (var DB = new SQLiteConnection(connection))
                {
                    DB.Open();
                    foreach (var script in migrationScripts)
                    {
                        var command = new SQLiteCommand(script, DB);
                        var rows = command.ExecuteNonQuery();

                        Trace.WriteLine($"Row(s) affected: {rows}");

                    }
                }

            }
            return false;
        }

        public static bool DeleteDB(string filePath)
        {
            return false;
        }
    }
}
