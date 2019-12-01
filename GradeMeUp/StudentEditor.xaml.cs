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

    }
}
