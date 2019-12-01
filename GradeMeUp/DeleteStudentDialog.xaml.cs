using GradeMeUp.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// Interaction logic for DeleteStudentDialog.xaml
    /// </summary>
    public partial class DeleteStudentDialog : Window
    {
        Student StudentDeleteInfo { get; set; }
        public event DataChangedEventHandler DataChanged;
        public delegate void DataChangedEventHandler(object sender, EventArgs e);

        public DeleteStudentDialog(Student student)
        {
            InitializeComponent();
            if (student != null)
            {
                StudentDeleteInfo = student;
                StudentDeleteName.Text = student.FullName;
            }
        }

        private void StudentDeleteOK_Click(object sender, RoutedEventArgs e)
        {
            var connection = new SQLiteConnection("Data Source=courses.sqlite;foreign keys=true;");

            var DB = new DBConnection(connection);
            var entryRemoved = DB.Table("Students").Delete(StudentDeleteInfo.ID);
            if (entryRemoved)
            {
                DataChanged?.Invoke(this, new EventArgs());
            }
        }

        private void StudentDeleteCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
