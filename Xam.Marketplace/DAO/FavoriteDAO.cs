using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xam.Marketplace.Extensions;
using Xam.Marketplace.Model;

namespace Xam.Marketplace.DAO
{
    public sealed class FavoriteDAO
    {
        private readonly SQLiteAsyncConnection _connection;
        public FavoriteDAO(SQLiteAsyncConnection connection)
        {
            _connection = connection;
            _connection.CreateTableAsync<Favorite>()
                .SafeFireAndForget(onException: ex => Debug.WriteLine(ex.Message));
        }

        public async Task<IEnumerable<Favorite>> GetAllAsync() {
            return await _connection.Table<Favorite>().ToListAsync();
        }

        public async Task SaveAsync(Favorite model) {
            await _connection.InsertAsync(model);
        }

        public async Task DeleteAsync(int id) {
            await _connection.DeleteAsync(id);
        }
    }
}
