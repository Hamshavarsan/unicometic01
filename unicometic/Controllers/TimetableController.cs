using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class TimetableController
    {
        private readonly DatabaseManager dbManager = new DatabaseManager();

        public void AddTimetable(int subjectId, string timeSlot, int roomId)
        {
            if (subjectId <= 0 || string.IsNullOrEmpty(timeSlot) || roomId <= 0)
                throw new ArgumentException("Invalid timetable data.");
            dbManager.AddTimetable(subjectId, timeSlot, roomId);
        }

        public DataTable GetTimetables()
        {
            var dt = new DataTable();
            using (var reader = dbManager.GetTimetables())
            {
                dt.Load(reader);
            }
            return dt;
        }

        public DataTable GetSubjects()
        {
            var dt = new DataTable();
            using (var reader = dbManager.GetSubjects())
            {
                dt.Load(reader);
            }
            return dt;
        }

        public DataTable GetRooms()
        {
            var dt = new DataTable();
            using (var reader = dbManager.GetRooms())
            {
                dt.Load(reader);
            }
            return dt;
        }
    }
}