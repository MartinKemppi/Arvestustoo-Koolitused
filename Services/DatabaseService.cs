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
            _database.CreateTableAsync<RegKursile>().Wait();
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
        public async Task<List<Koolitus>> GetCoursesAsync()
        {
            var courses = await _database.Table<Koolitus>().ToListAsync();
            foreach (var course in courses)
            {
                var teacher = await GetTeacherByIdAsync(course.OpetajaId);
                if (teacher != null)
                {
                    course.Opetajanimi = teacher.Opetajanimi;
                }
            }
            return courses;
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
        public async Task<Koolitus> GetCourseByIdAsync(int courseId)
        {
            try
            {
                return await _database.Table<Koolitus>().FirstOrDefaultAsync(course => course.Id == courseId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get course by ID from database: {ex.Message}");
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
        public async Task<Opetaja> GetTeacherByUsernameAsync(string username)
        {
            try
            {
                var user = await _database.Table<Kasutaja>().FirstOrDefaultAsync(u => u.Kasutajanimi == username);
                if (user == null) throw new Exception("Kasutajat ei leitud.");

                return await _database.Table<Opetaja>().FirstOrDefaultAsync(o => o.Id == user.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Õpetaja laadimine ebaõnnestus: {ex.Message}");
            }
        }

        public async Task<List<Koolitus>> GetCoursesByTeacherIdAsync(int teacherId)
        {
            try
            {
                return await _database.Table<Koolitus>().Where(k => k.OpetajaId == teacherId).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Kursuste laadimine ebaõnnestus: {ex.Message}");
            }
        }

        public async Task<List<RegKursile>> GetStudentsByCourseIdAsync(int courseId)
        {
            try
            {
                var students = await _database.Table<RegKursile>().Where(r => r.CourseId == courseId).ToListAsync();
                foreach (var student in students)
                {
                    student.CourseName = (await _database.Table<Koolitus>().FirstOrDefaultAsync(k => k.Id == courseId))?.Koolitusnimi;
                }
                return students;
            }
            catch (Exception ex)
            {
                throw new Exception($"Õpilaste laadimine ebaõnnestus: {ex.Message}");
            }
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

        //Registreerimine kursile
        public async Task RegisterCourseAsync(RegKursile regInfo)
        {
            await _database.InsertAsync(regInfo);
        }
        public async Task<List<RegKursile>> GetRegisteredCoursesAsync(string username)
        {
            try
            {
                return await _database.Table<RegKursile>().Where(x => x.Username == username).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to get registered courses: {ex.Message}");
            }
        }
        public async Task DeleteRegistrationAsync(RegKursile registration)
        {
            try
            {
                await _database.DeleteAsync(registration);
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось удалить регистрацию курса: {ex.Message}");
            }
        }
        public async Task<List<RegKursile>> GetAllRegistrationsAsync()
        {
            try
            {
                var registrations = await _database.Table<RegKursile>().ToListAsync();

                foreach (var registration in registrations)
                {
                    var course = await _database.Table<Koolitus>().FirstOrDefaultAsync(c => c.Id == registration.CourseId);

                    if (course != null)
                    {
                        registration.CourseName = course.Koolitusnimi;
                    }
                }

                return registrations;
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось получить все записи из таблицы RegKursile: {ex.Message}");
            }
        }

    }
}