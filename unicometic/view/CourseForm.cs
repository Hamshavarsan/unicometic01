using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;

namespace UnicomTICManagementSystem.Views
{
    public partial class CourseForm : Form
    {
        private readonly CourseController courseController;
        private readonly string role;

        public CourseForm(string role)
        {
            this.role = role;
            courseController = new CourseController();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Course Management";
            this.Size = new System.Drawing.Size(500, 400);

            var lblCourseName = new Label { Text = "Course Name:", Location = new System.Drawing.Point(20, 20) };
            var txtCourseName = new TextBox { Name = "txtCourseName", Location = new System.Drawing.Point(100, 20), Width = 200 };

            var btnAdd = new Button { Text = "Add Course", Location = new System.Drawing.Point(100, 60), Width = 100 };
            btnAdd.Click += BtnAdd_Click;

            var dgvCourses = new DataGridView
            {
                Name = "dgvCourses",
                Location = new System.Drawing.Point(20, 100),
                Size = new System.Drawing.Size(400, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var controls = new Control[] { dgvCourses };
            if (role == "Admin")
            {
                controls = new Control[] { lblCourseName, txtCourseName, btnAdd, dgvCourses };
            }

            this.Controls.AddRange(controls);
            LoadCourses();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var txtCourseName = this.Controls["txtCourseName"] as TextBox;
            if (string.IsNullOrEmpty(txtCourseName.Text))
            {
                MessageBox.Show("Please enter a course name.", "Error");
                return;
            }

            courseController.AddCourse(txtCourseName.Text);
            MessageBox.Show("Course added successfully.", "Success");
            txtCourseName.Clear();
            LoadCourses();
        }

        private void LoadCourses()
        {
            var dgvCourses = this.Controls["dgvCourses"] as DataGridView;
            dgvCourses.DataSource = courseController.GetCourses();
        }
    }
}
