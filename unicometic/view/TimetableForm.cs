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

namespace UnicomTICManagementSystem.Views
{
    public partial class TimetableForm : Form
    {
        private readonly TimetableController timetableController;
        private readonly string role;

        public TimetableForm(string role)
        {
            this.role = role;
            timetableController = new TimetableController();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Timetable Management";
            this.Size = new System.Drawing.Size(600, 400);

            var lblSubject = new Label { Text = "Subject:", Location = new System.Drawing.Point(20, 20) };
            var cmbSubject = new ComboBox { Name = "cmbSubject", Location = new System.Drawing.Point(100, 20), Width = 200 };

            var lblTimeSlot = new Label { Text = "Time Slot:", Location = new System.Drawing.Point(20, 60) };
            var txtTimeSlot = new TextBox { Name = "txtTimeSlot", Location = new System.Drawing.Point(100, 60), Width = 200 };

            var lblRoom = new Label { Text = "Room:", Location = new System.Drawing.Point(20, 100) };
            var cmbRoom = new ComboBox { Name = "cmbRoom", Location = new System.Drawing.Point(100, 100), Width = 200 };

            var btnAdd = new Button { Text = "Add Timetable", Location = new System.Drawing.Point(100, 140), Width = 100 };
            btnAdd.Click += BtnAdd_Click;

            var dgvTimetables = new DataGridView
            {
                Name = "dgvTimetables",
                Location = new System.Drawing.Point(20, 180),
                Size = new System.Drawing.Size(500, 150),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var controls = new Control[] { dgvTimetables };
            if (role == "Admin")
            {
                controls = new Control[] { lblSubject, cmbSubject, lblTimeSlot, txtTimeSlot, lblRoom, cmbRoom, btnAdd, dgvTimetables };
            }

            this.Controls.AddRange(controls);
            LoadTimetables();
            if (role == "Admin")
            {
                LoadSubjects(cmbSubject);
                LoadRooms(cmbRoom);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var cmbSubject = this.Controls["cmbSubject"] as ComboBox;
            var txtTimeSlot = this.Controls["txtTimeSlot"] as TextBox;
            var cmbRoom = this.Controls["cmbRoom"] as ComboBox;

            if (cmbSubject.SelectedIndex == -1 || string.IsNullOrEmpty(txtTimeSlot.Text) || cmbRoom.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a subject, enter a time slot, and select a room.", "Error");
                return;
            }

            var subjectId = (int)cmbSubject.SelectedValue;
            var timeSlot = txtTimeSlot.Text;
            var roomId = (int)cmbRoom.SelectedValue;

            timetableController.AddTimetable(subjectId, timeSlot, roomId);
            MessageBox.Show("Timetable added successfully.", "Success");
            txtTimeSlot.Clear();
            LoadTimetables();
        }

        private void LoadTimetables()
        {
            var dgvTimetables = this.Controls["dgvTimetables"] as DataGridView;
            dgvTimetables.DataSource = timetableController.GetTimetables();
        }

        private void LoadSubjects(ComboBox cmbSubject)
        {
            var subjects = timetableController.GetSubjects();
            cmbSubject.DataSource = subjects;
            cmbSubject.DisplayMember = "SubjectName";
            cmbSubject.ValueMember = "SubjectID";
        }

        private void LoadRooms(ComboBox cmbRoom)
        {
            var rooms = timetableController.GetRooms();
            cmbRoom.DataSource = rooms;
            cmbRoom.DisplayMember = "RoomName";
            cmbRoom.ValueMember = "RoomID";
        }
    }
}
