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
    public class CourseController
    {
        private readonly DatabaseManager dbManager = new DatabaseManager();

        public void AddCourse(string courseName)
        {
            if (string.IsNullOrEmpty(courseName))
                throw new ArgumentException("Course name cannot be empty.");
            dbManager.AddCourse(courseName);
        }

        public DataTable GetCourses()
        {
            var dt = new DataTable();
            using (var reader = dbManager.GetCourses())
            {
                dt.Load(reader);
            }
            return dt;
        }
    }
}
