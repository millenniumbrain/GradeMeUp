using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
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
using System.Windows.Threading;
using System.Threading;

namespace GradeMeUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private class AssignmentTypeListItem
        {
            public int AssigntmentType { get; set; }
            public string AssignmentTypeName { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();

            DBSetup.MigrateDB();

            var writeConnection = $"Data Source=courses.sqlite";
            var DB = new SQLiteConnection(writeConnection);

            var newStudents = new StudentGenerator(DB);
            var newCourses = new CourseGenerator(DB);
            var newAssignments = new AssignmentGenerator(DB);
            var newCourseStudents = new CourseStudentGenerator(DB);

            
            newCourses.Generate(10);
            newStudents.Generate(10);
            newAssignments.Generate(10);
            newCourseStudents.Generate(10);

            var students = Student.All();
            var courses = Course.All();
            var assignments = Assignment.All();

            StudentsListBoxView.ItemsSource = students;

            foreach (var course in courses)
            {
                course.CalculateGrades();
            }
            StudentsCourseListView.ItemsSource = courses;

            AssignmentsListView.ItemsSource = assignments;
        }

        private void StudentsListView_DoubleClick(object sender, MouseEventArgs e)
        {
            var studentsListBox = e.Source as ListBoxItem;
            var item = ((FrameworkElement)e.OriginalSource).DataContext as Student;
        }

        private void StudentsListView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var student = ((FrameworkElement)e.OriginalSource).DataContext as Student;
            StudentsCourseListView.ItemsSource = student.Courses;
        }

        private void AssignmentsListView_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var assignmentTypeItem = e.Source as ListBoxItem;
            var assignmentType = Convert.ToInt32(assignmentTypeItem.Tag.ToString());
            var assignments = Assignment.GetAssignmentsByType(assignmentType);
            AssignmentsListView.ItemsSource = assignments;
        }

        private void StudentsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            // take into account vertical scrollbar
            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            var col1 = 0.10;
            var col2 = 0.40;
            var col3 = 0.15;
            var col4 = 0.15;

            gridView.Columns[0].Width = workingWidth * col1;
            gridView.Columns[1].Width = workingWidth * col2;
            gridView.Columns[2].Width = workingWidth * col3;
            gridView.Columns[3].Width = workingWidth * col4;
        }

        private void AssignmentsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            // take into account vertical scrollbar
            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; 
            var col1 = 0.70;
            var col2 = 0.15;
            var col3 = 0.15;

            gridView.Columns[0].Width = workingWidth * col1;
            gridView.Columns[1].Width = workingWidth * col2;
            gridView.Columns[2].Width = workingWidth * col3;
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
