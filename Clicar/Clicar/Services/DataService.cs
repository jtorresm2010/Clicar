using Clicar.Interface;
using Clicar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Clicar.Services
{



    class DataService
    {
        #region Variables
        private SQLiteAsyncConnection connection;

        #endregion


        public DataService()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            connection = new SQLiteAsyncConnection(databasePath);
            OpenOrCreateDB();
        }

        private async Task OpenOrCreateDB()
        {
            await connection.CreateTableAsync<Maestro>().ConfigureAwait(false);
            await connection.CreateTableAsync<Usuario>().ConfigureAwait(false);
            await connection.CreateTableAsync<AreasInspeccion>().ConfigureAwait(false);
            await connection.CreateTableAsync<Sucursal>().ConfigureAwait(false);
        }

        #region Metodos Genericos
        public async Task Insert<T>(T model)
        {
            await this.connection.InsertOrReplaceAsync(model);
        }
        public async Task Insert<T>(List<T> models)
        {
            await this.connection.InsertAllAsync(models);
        }
        public async Task Update<T>(T model)
        {
            await this.connection.UpdateAsync(model);
        }
        public async Task Update<T>(List<T> models)
        {
            await this.connection.UpdateAllAsync(models);
        }
        public async Task Delete<T>(T model)
        {
            await this.connection.DeleteAsync(model);
        }
        #endregion

        #region Consultas

        public async Task<List<Sucursal>> GetAllSucursales()
        {
            var query = await this.connection.QueryAsync<Sucursal>("select * from [Sucursal]");

            return query;
        }



        #endregion


    }
}
