using Clicar.Interface;
using Clicar.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Clicar.Services
{



    public class DataService
    {
        #region Variables
        private SQLiteAsyncConnection connection;

        #endregion


        public DataService()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            connection = new SQLiteAsyncConnection(databasePath);
            //OpenOrCreateDB();
        }

        public async Task OpenOrCreateDB()
        {

            await connection.CreateTableAsync<ItemsAreasInspeccionDB>().ConfigureAwait(false);
            await connection.CreateTableAsync<SucursalDB>().ConfigureAwait(false);
            await connection.CreateTableAsync<Maestro>().ConfigureAwait(false);
            await connection.CreateTableAsync<Usuario>().ConfigureAwait(false);
            await connection.CreateTableAsync<AreasInspeccion>().ConfigureAwait(false);
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

        public async Task<List<SucursalDB>> GetAllSucursales()
        {
            var query = await this.connection.QueryAsync<SucursalDB>("select * from [SucursalDB]");

            return query;
        }

        public async Task<Maestro> GetMaestro()
        {
            var query = await this.connection.QueryAsync<Maestro>("select * from [Maestro]");

            var maestro = query[0];

            return maestro;
        }

        public async Task<List<AreasInspeccion>> GetAreasInspeccion()
        {
            var query = await this.connection.QueryAsync<AreasInspeccion>("select * from [AreasInspeccion]");

            return query;
        }



        public async Task ResetTable()
        {
            var query = await this.connection.QueryAsync<ItemsAreasInspeccionDB>("delete * from [ItemsAreasInspeccionDB]");

        }


        public async Task<List<ItemsAreasInspeccionDB>> GetItemsInspeccion()
        {
            var query = await this.connection.QueryAsync<ItemsAreasInspeccionDB>("select * from [ItemsAreasInspeccionDB]");

            return query;
        }

        #endregion


    }
}
