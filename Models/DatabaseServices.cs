using SQLite;
using Campuscloset.Models; // Reference your model namespace
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Campuscloset.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Item>().Wait();
        }

        public async Task<int> AddItemAsync(Item item)
        {
            try
            {
                var result = await _database.InsertAsync(item);
                System.Diagnostics.Debug.WriteLine($"AddItemAsync Result: {result}");
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in AddItemAsync: {ex.Message}");
                throw; // Re-throw to surface errors
            }
        }


        public Task<List<Item>> GetItemsAsync()
        {
            return _database.Table<Item>().ToListAsync();
        }
    }
}
