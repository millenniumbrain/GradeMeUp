using GradeMeUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GradeMeUp
{
    /// <summary>
    /// Interaction logic for StudentEditor.xaml
    /// </summary>
    public partial class StudentEditor : Window
    {
        Student StudentInfo { get; set; }
        public StudentEditor(Student student = null)
        {
            InitializeComponent();
            if (student != null)
            {
                StudentFirstName.Text = student.FirstName;
                StudentLastName.Text = student.LastName;
            }
        }

        private void StudentsCourseList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            // take into account vertical scrollbar
            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
            var col1 = 0.25;
            var col2 = 0.75;

            gridView.Columns[0].Width = workingWidth * col1;
            gridView.Columns[1].Width = workingWidth * col2;

        }

        private void StudentAssignmentList_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

    }
}
