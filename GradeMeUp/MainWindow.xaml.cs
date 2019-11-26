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
        private CancellationTokenSource CancellationToken;
        private readonly TimeSpan DoublClickDelay = TimeSpan.FromSeconds(0.2);

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
            var assignments = Assignment.All();

            StudentsListBoxView.ItemsSource = students;
            foreach (var course in courses)
            {
                course.CalculateGrades();
            }
            
            StudentsCourseListView.ItemsSource = courses;
            StudentsCourseListView.SizeChanged += new System.Windows.SizeChangedEventHandler(StudentsListView_SizeChanged);

            AssignmentsListView.ItemsSource = assignments;
            AssignmentsListView.SizeChanged += new System.Windows.SizeChangedEventHandler(AssignmentsListView_SizeChanged);
        }

        private void StudentListView_LeftClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                try
                {
                    CancellationToken = new CancellationTokenSource();
                    var token = CancellationToken.Token;

                    Task.Run(async () =>
                    {
                        await Task.Delay(DoublClickDelay, token);

                        Trace.WriteLine("Single Click");
                    }, token);
                }
                catch (TaskCanceledException)
                {

                }

                return;


            } 

            if (CancellationToken != null)
            {
                CancellationToken.Cancel();
            }
            //var studentsListBox = e.Source as ListBoxItem;
            //var item = ((FrameworkElement)e.OriginalSource).DataContext as Student;
            //Trace.WriteLine($"The students LEFT CLICK name is: {item.FirstName}");
            Trace.WriteLine("Double Click occured.");
        }

        private void StudentsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.10;
            var col2 = 0.40;
            var col3 = 0.15;
            var col4 = 0.15;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
            gView.Columns[3].Width = workingWidth * col4;
        }

        private void AssignmentsListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.70;
            var col2 = 0.15;
            var col3 = 0.15;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
        }
    }
}
