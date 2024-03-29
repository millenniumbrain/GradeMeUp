﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GradeMeUp
{
    public class Assignment
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public int? Grade { get; set; }
        public int PointsPossible { get; set; }
        public int AssignmentType { get; set; }
        public string DisplayAssignmentType 
        { 
            get 
            {
                var assignmentType = (AssignmentType)AssignmentType;
                return Regex.Replace(assignmentType.ToString(), "([a-z])([A-Z])", "$1 $2");
            } 
        }
        public long? StudentID { get; set; }
        public long? CourseID { get; set; }

        public static List<Assignment> GetAssignmentsByType(int assignmentType)
        {
            var connection = $"Data Source=courses.sqlite;foreign keys=true;";
            string allAssignments;
            if (assignmentType == 0)
            {
                allAssignments = "SELECT * FROM Assignments";
            } 
            else
            {
                allAssignments = $"SELECT * FROM Assignments WHERE AssignmentType = {assignmentType}";
            }

            using (var DB = new SQLiteConnection(connection))
            {
                DB.Open();
                var assignments = new List<Assignment>();

                using (var command = new SQLiteCommand(allAssignments, DB))
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
                        else
                        {
                            assignment.Grade = (int)result[nameof(assignment.Grade)];
                        }

                        if (result[nameof(assignment.PointsPossible)] == DBNull.Value)
                        {
                            assignment.PointsPossible = 0;
                        }
                        else
                        {
                            assignment.PointsPossible = (int)result[nameof(assignment.PointsPossible)];
                        }

                        assignment.AssignmentType = Convert.ToInt32(result[nameof(assignment.AssignmentType)].ToString());

                        assignments.Add(assignment);
                    }
                }

                return assignments;
            }
        }

        public static List<Assignment> All()
        {
            var connection = $"Data Source=courses.sqlite;foreign keys=true;";
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
                        else
                        {
                            assignment.Grade = (int)result[nameof(assignment.Grade)];
                        }

                        if (result[nameof(assignment.PointsPossible)] == DBNull.Value)
                        {
                            assignment.PointsPossible = 0;
                        }
                        else
                        {
                            assignment.PointsPossible = (int)result[nameof(assignment.PointsPossible)];
                        }

                        assignment.AssignmentType  = Convert.ToInt32(result[nameof(assignment.AssignmentType)].ToString());

                        assignments.Add(assignment);
                    }

                }

                return assignments;
            }
        }
    }
}
