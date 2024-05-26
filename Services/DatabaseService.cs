using SQLite;
using System.IO;
using System.Threading.Tasks;
using Koolitused.Models;
using System.Collections.Generic;

namespace Koolitused.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Koolitused.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Kasutaja>().Wait();
            _database.CreateTableAsync<Koolitus>().Wait();
            _database.CreateTableAsync<Opetaja>().Wait();
        }

        //Kasutaja
        public Task<int> SaveUserAsync(Kasutaja user)
        {
            return _database.InsertAsync(user);
        }
        public Task<Kasutaja> GetUserAsync(string username, string password)
        {
            return _database.Table<Kasutaja>()
                            .FirstOrDefaultAsync(u => u.Kasutajanimi == username && u.Kasutajasalasona == password);
        }
        public Task<bool> UserExistsAsync(string kasutajanimi)
        {
            return _database.Table<Kasutaja>()
                            .Where(u => u.Kasutajanimi == kasutajanimi)
                            .FirstOrDefaultAsync()
                            .ContinueWith(t => t.Result != null);
        }
        public Task<int> DeleteUserAsync(Kasutaja user)
        {
            return _database.DeleteAsync(user);
        }
        public Task<List<Kasutaja>> GetUsersAsync()
        {
            return _database.Table<Kasutaja>().ToListAsync();
        }
        public Task<int> UpdateUserAsync(Kasutaja user)
        {
            return _database.UpdateAsync(user);
        }
        public Task<Kasutaja> GetUserByUsernameAsync(string username)
        {
            return _database.Table<Kasutaja>()
                            .Where(u => u.Kasutajanimi == username)
                            .FirstOrDefaultAsync();
        }

        //Koolitus
        public Task<List<Koolitus>> GetCoursesAsync()
        {
            return _database.Table<Koolitus>().ToListAsync();
        }
        public Task<int> SaveCourseAsync(Koolitus course)
        {
            return _database.InsertAsync(course);
        }
        public Task<int> UpdateCourseAsync(Koolitus course)
        {
            return _database.UpdateAsync(course);
        }
        public Task<int> DeleteCourseAsync(Koolitus course)
        {
            return _database.DeleteAsync(course);
        }
        public async Task<List<string>> GetCourseNamesAsync()
        {
            var courses = await _database.Table<Koolitus>().ToListAsync();
            var courseNames = new List<string>();
            foreach (var course in courses)
            {
                courseNames.Add(course.Koolitusnimi);
            }
            return courseNames;
        }
        public async Task<Koolitus> GetCourseByNameTeacherAndDateAsync(string courseName, int teacherId, string date)
        {
            try
            {
                var courseList = await _database.Table<Koolitus>()
                    .Where(x => x.Koolitusnimi == courseName && x.OpetajaId == teacherId && x.Kuupaev == date)
                    .ToListAsync();

                return courseList.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении курса из БД: {ex.Message}");
                return null;
            }
        }

        //Opetaja
        public Task<int> SaveTeacherAsync(Opetaja teacher)
        {
            return _database.InsertAsync(teacher);
        }
        public Task<int> UpdateTeacherAsync(Opetaja teacher)
        {
            return _database.UpdateAsync(teacher);
        }
        //public async Task DropOpetajaTableAsync()
        //{
        //    await _database.DropTableAsync<Opetaja>();
        //}
        public Task<List<Opetaja>> GetTeachersAsync()
        {
            return _database.Table<Opetaja>().ToListAsync();
        }
        public Task<int> DeleteTeacherAsync(Opetaja teacher)
        {
            return _database.DeleteAsync(teacher);

        }
        public async Task<Opetaja> GetTeacherByIdAsync(int teacherId)
        {
            return await _database.Table<Opetaja>().FirstOrDefaultAsync(teacher => teacher.Id == teacherId);
        }
        public async Task<Opetaja> GetTeacherByNameAsync(string teacherName)
        {
            return await _database.Table<Opetaja>().FirstOrDefaultAsync(t => t.Opetajanimi == teacherName);
        }

        //Roll
        public Task<List<Roll>> GetRolesAsync()
        {
            return _database.Table<Roll>().ToListAsync();
        }
        public Task<int> SaveRoleAsync(Roll role)
        {
            return _database.InsertAsync(role);
        }
        public Task<int> UpdateRoleAsync(Roll role)
        {
            return _database.UpdateAsync(role);
        }
        public Task<int> DeleteRoleAsync(Roll role)
        {
            return _database.DeleteAsync(role);
        }
        public Task<Roll> GetRoleByNameAsync(string roleName)
        {
            return _database.Table<Roll>()
                            .Where(r => r.Rollnimi == roleName)
                            .FirstOrDefaultAsync();
        }
    }
}