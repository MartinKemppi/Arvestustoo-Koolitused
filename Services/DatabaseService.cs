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
            _database.CreateTableAsync<Roll>().Wait();
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