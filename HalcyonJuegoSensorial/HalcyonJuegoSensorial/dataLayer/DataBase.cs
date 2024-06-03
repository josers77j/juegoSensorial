using HalcyonJuegoSensorial.modelLayer;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HalcyonJuegoSensorial.dataLayer
{
    public class DataBase
    {
        private readonly SQLiteAsyncConnection _database;

        public DataBase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "MyApp.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ModelUser>().Wait();
        }

        public Task<int> SaveUsuarioAsync(ModelUser usuario)
        {
            if (usuario.Id != 0)
            {
                return _database.UpdateAsync(usuario);
            }
            else
            {
                return _database.InsertAsync(usuario);
            }
        }

        public Task<ModelUser> GetUsuarioAsync(int id)
        {
            return _database.Table<ModelUser>().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> DeleteUsuarioAsync(ModelUser usuario)
        {
            return _database.DeleteAsync(usuario);
        }

        public Task<List<ModelUser>> GetUsuariosAsync()
        {
            return _database.Table<ModelUser>().ToListAsync();
        }

        public Task<ModelUser> GetUsuarioByNameAsync(string nombreUsuario)
        {
            return _database.Table<ModelUser>().Where(u => u.NombreUsuario == nombreUsuario).FirstOrDefaultAsync();
        }

    }
}
