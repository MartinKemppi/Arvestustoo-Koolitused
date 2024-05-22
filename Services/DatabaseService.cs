using SQLite;
using System.IO;
using System.Threading.Tasks;
using Koolitused.Models;

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
            _database.CreateTableAsync<RegKursile>().Wait();
        }

        public Task<int> SaveUserAsync(Kasutaja user)
        {
            return _database.InsertAsync(user);
        }

        public Task<Kasutaja> GetUserAsync(string username, string password)
        {
            return _database.Table<Kasutaja>()
                            .FirstOrDefaultAsync(u => u.Kasutajanimi == username && u.Kasutajasalasona == password);
        }

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

        public async Task<bool> UserExistsAsync(string username)
        {
            var user = await GetUserByUsernameAsync(username);
            return user != null;
        }

        private Task<Kasutaja> GetUserByUsernameAsync(string username)
        {
            return _database.Table<Kasutaja>().FirstOrDefaultAsync(u => u.Kasutajanimi == username);
        }
    }
}
