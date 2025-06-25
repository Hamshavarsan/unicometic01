using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Controllers;
using UnicomTICManagementSystem.Models;

namespace UnicomTICManagementSystem.Views
{
    public partial class StudentForm : Form
    {
        private readonly StudentController _studentController = new StudentController();
        private readonly CourseController _courseController = new CourseController();
        private int selectedStudentId = -1;

        public StudentForm()
        {
            InitializeComponent();
            LoadCourses();
            LoadStudents();
        }

        private async void LoadCourses()
        {
            var courses = await _courseController.GetAllCoursesAsync();
            cmbCourse.DataSource = courses;
            cmbCourse.DisplayMember = "CourseName";
            cmbCourse.ValueMember = "CourseID";
        }

        private async void LoadStudents()
        {
            var students = await _studentController.GetAllStudentsAsync();
            dgvStudents.DataSource = students;
        }

        private async void btnAddStudent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentName.Text))
            {
                MessageBox.Show("Student name cannot be empty.");
                return;
            }

            var student = new Student
            {
                Name = txtStudentName.Text.Trim(),
                CourseID = (int)cmbCourse.SelectedValue
            };

            await _studentController.AddStudentAsync(student);
            LoadStudents();
            txtStudentName.Clear();
        }

        private async void btnEditStudent_Click(object sender, EventArgs e)
        {
            if (selectedStudentId == -1)
            {
                MessageBox.Show("Select a student to edit.");
                return;
            }

            var student = new Student
            {
                StudentID = selectedStudentId,
                Name = txtStudentName.Text.Trim(),
                CourseID = (int)cmbCourse.SelectedValue
            };

            await _studentController.UpdateStudentAsync(student);
            LoadStudents();
            txtStudentName.Clear();
            selectedStudentId = -1;
        }

        private async void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (selectedStudentId == -1)
            {
                MessageBox.Show("Select a student to delete.");
                return;
            }

            await _studentController.DeleteStudentAsync(selectedStudentId);
            LoadStudents();
            txtStudentName.Clear();
            selectedStudentId = -1;
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedStudentId = Convert.ToInt32(dgvStudents.Rows[e.RowIndex].Cells["StudentID"].Value);
                txtStudentName.Text = dgvStudents.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                cmbCourse.SelectedValue = Convert.ToInt32(dgvStudents.Rows[e.RowIndex].Cells["CourseID"].Value);
            }
        }
    }
}

