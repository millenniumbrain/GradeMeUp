using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using GradeMeUp.Models;
using GradeMeUp.Generators;

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
            DBSetup.MigrateDB();

            var writeConnection = $"Data Source=courses.sqlite";
            var DB = new SQLiteConnection(writeConnection);

            var newStudents = new StudentGenerator(DB);
            newStudents.Generate(10);
            var newCourses = new CourseGenerator(DB);
            newCourses.Generate(10);
            var newAssignments = new AssignmentGenerator(DB);
            newAssignments.Generate(10);

            var students = Student.All();
            var courses = Course.All();

            var studentNames = new List<string>();
            foreach (var student in students)
            {
                var fullName = $"{student.FirstName} {student.LastName}";
                studentNames.Add(fullName);
            }
            StudentsTreeView.ItemsSource = studentNames;
            StudentsCourseListView.ItemsSource = courses;
        }
    }
}
