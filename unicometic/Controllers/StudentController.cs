using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using UnicomTICManagementSystem.Models;
using UnicomTICManagementSystem.Repositories;

namespace UnicomTICManagementSystem.Controllers
{
    public class StudentController
    {
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            List<Student> students = new List<Student>();

            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM Students";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        students.Add(new Student
                        {
                            StudentID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            CourseID = reader.GetInt32(2)
                        });
                    }
                }
            }

            return students;
        }

        public async Task AddStudentAsync(Student student)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "INSERT INTO Students (Name, CourseID) VALUES (@name, @courseId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@courseId", student.CourseID);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateStudentAsync(Student student)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "UPDATE Students SET Name = @name, CourseID = @courseId WHERE StudentID = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@courseId", student.CourseID);
                    cmd.Parameters.AddWithValue("@id", student.StudentID);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "DELETE FROM Students WHERE StudentID = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", studentId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
