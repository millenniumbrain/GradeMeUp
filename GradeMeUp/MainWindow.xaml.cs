using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GradeMeUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var assignments = new List<Assignment> {
                new Assignment("HW 1", 0, 100),
                new Assignment("HW 2", 60, 60)
            };

            var items = new List<Student>
            {
                new Student("William Wilson", 90),
                new Student("Steve Ballmer", 100)
            };

            var studentAssignments = new DataTable();

            var upperCaseSplit = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            studentAssignments.Columns.Add(new DataColumn("Student", typeof(string)));
            studentAssignments.Columns.Add(new DataColumn("Overall Score", typeof(int)));

            foreach (var assignment in assignments)
            {
                var assignmentName = new DataColumn
                {
                    DataType = typeof(string),
                    ColumnName = assignment.Name
                };


                studentAssignments.Columns.Add(assignmentName);
            }

            foreach (var student in items)
            {

                var row = studentAssignments.NewRow();
                
                row["Student"] = student.Name;
                row["Overall Score"] = 0;
                foreach (var assignment in assignments)
                {
                    if (assignment.Points > 0)
                    {
                        row[assignment.Name] = assignment.PointsPossible.ToString();
                    } else
                    {
                        row[assignment.Name] = "-";
                    }
                }
                studentAssignments.Rows.Add(row);


            }


            allCourses.ItemsSource = studentAssignments.DefaultView;



            allCourses.Items.Refresh();


        }

        private void allCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
