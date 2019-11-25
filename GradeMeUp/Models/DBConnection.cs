using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Runtime;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeMeUp.Models
{
    public class DBConnection
    {
        public string TableName { get; set; }
        public SQLiteConnection Connection { get; set; }

        public DBConnection(SQLiteConnection connection)
        {
            Connection = new SQLiteConnection(connection);
        }

        public DBConnection Table(string table)
        {
            TableName = table;
            return this;
        }

        public bool Insert(Dictionary<string, object> tableColumns)
        {
            string insertString = $"INSERT INTO {TableName}";
            string columns = "(";
            string values = "VALUES(";

            if (tableColumns != null)
            {
                var i = 0;
                foreach (var entry in tableColumns)
                {
                    columns += $"{entry.Key},";
                    if (entry.Value is string)
                    {
                        values += $"'{entry.Value.ToString().Replace("'", "''")}',";
                    }
                    else
                    {
                        values += $"'{entry.Value}',";

                    }
                    i++;
                    if (tableColumns.Count == i)
                    {
                        columns = columns.Remove(columns.Length - 1);
                        columns += ")";
                        values = values.Remove(values.Length - 1);
                        values += ")";
                    }
                }
            }

            insertString = $"{insertString}{columns}{values};";
            if (Connection != null)
            {
                using (Connection)
                {
                    Connection.Open();
                    using (var command = new SQLiteCommand(insertString, Connection))
                    {
                        var rows = command.ExecuteNonQuery();

                        if (rows == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
            }
            return false;
        }
    }
}
