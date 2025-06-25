using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UnicomTICManagementSystem.Views
{
    public partial class MainForm : Form
    {
        private readonly string role;

        public MainForm(string role)
        {
            this.role = role;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Unicom TIC Dashboard";
            this.Size = new System.Drawing.Size(400, 300);

            var btnCourses = new Button { Text = "Manage Courses", Location = new System.Drawing.Point(20, 20), Width = 150 };
            btnCourses.Click += (s, e) => new CourseForm(role).ShowDialog();

            var btnTimetable = new Button { Text = "Manage Timetable", Location = new System.Drawing.Point(20, 80), Width = 150 };
            btnTimetable.Click += (s, e) => new TimetableForm(role).ShowDialog();

            var controls = new Control[] { btnTimetable };
            if (role == "Admin")
            {
                controls = new Control[] { btnCourses, btnTimetable };
            }

            this.Controls.AddRange(controls);
        }
    }
}
