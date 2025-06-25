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
    public class SubjectController
    {
        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            List<Subject> subjects = new List<Subject>();

            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM Subjects";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        subjects.Add(new Subject
                        {
                            SubjectID = reader.GetInt32(0),
                            SubjectName = reader.GetString(1),
                            CourseID = reader.GetInt32(2)
                        });
                    }
                }
            }

            return subjects;
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "INSERT INTO Subjects (SubjectName, CourseID) VALUES (@name, @courseId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@courseId", subject.CourseID);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateSubjectAsync(Subject subject)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "UPDATE Subjects SET SubjectName = @name, CourseID = @courseId WHERE SubjectID = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", subject.SubjectName);
                    cmd.Parameters.AddWithValue("@courseId", subject.CourseID);
                    cmd.Parameters.AddWithValue("@id", subject.SubjectID);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteSubjectAsync(int subjectId)
        {
            using (var conn = DatabaseManager.GetConnection())
            {
                await conn.OpenAsync();
                string query = "DELETE FROM Subjects WHERE SubjectID = @id";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", subjectId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
