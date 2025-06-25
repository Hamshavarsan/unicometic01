using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace UnicomTICManagementSystem.Repositories
{
    public static class DatabaseManager
    {
        private static readonly string dbPath = "unicomtic.db";
        private static readonly string connectionString = $"Data Source={dbPath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static async Task InitializeDatabaseAsync()
        {
            if (!File.Exists(dbPath))
                SQLiteConnection.CreateFile(dbPath);

            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                string[] tableCommands = new string[]
                {
                    @"CREATE TABLE IF NOT EXISTS Users (
                        UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL,
                        Password TEXT NOT NULL,
                        Role TEXT NOT NULL
                    )",
                    @"CREATE TABLE IF NOT EXISTS Courses (
                        CourseID INTEGER PRIMARY KEY AUTOINCREMENT,
                        CourseName TEXT NOT NULL
                    )",
                    @"CREATE TABLE IF NOT EXISTS Subjects (
                        SubjectID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SubjectName TEXT NOT NULL,
                        CourseID INTEGER,
                        FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
                    )",
                    @"CREATE TABLE IF NOT EXISTS Students (
                        StudentID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        CourseID INTEGER,
                        FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
                    )",
                    @"CREATE TABLE IF NOT EXISTS Exams (
                        ExamID INTEGER PRIMARY KEY AUTOINCREMENT,
                        ExamName TEXT NOT NULL,
                        SubjectID INTEGER,
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
                    )",
                    @"CREATE TABLE IF NOT EXISTS Marks (
                        MarkID INTEGER PRIMARY KEY AUTOINCREMENT,
                        StudentID INTEGER,
                        ExamID INTEGER,
                        Score INTEGER,
                        FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
                        FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)
                    )",
                    @"CREATE TABLE IF NOT EXISTS Rooms (
                        RoomID INTEGER PRIMARY KEY AUTOINCREMENT,
                        RoomName TEXT NOT NULL,
                        RoomType TEXT NOT NULL
                    )",
                    @"CREATE TABLE IF NOT EXISTS Timetables (
                        TimetableID INTEGER PRIMARY KEY AUTOINCREMENT,
                        SubjectID INTEGER,
                        TimeSlot TEXT NOT NULL,
                        RoomID INTEGER,
                        FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
                        FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID)
                    )"
                };

                foreach (var commandText in tableCommands)
                {
                    using var cmd = new SQLiteCommand(commandText, connection);
                    await cmd.ExecuteNonQueryAsync();
                }

                // Add default Admin user
                string insertAdmin = "INSERT OR IGNORE INTO Users (UserID, Username, Password, Role) VALUES (1, 'admin', 'admin123', 'Admin')";
                using var insertCmd = new SQLiteCommand(insertAdmin, connection);
                await insertCmd.ExecuteNonQueryAsync();
            }
        }
    }
}
