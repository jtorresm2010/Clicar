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
            await connection.CreateTableAsync<ItemsdetalleinspeccionDB>().ConfigureAwait(false);
            await connection.CreateTableAsync<ItemsAreasInspeccionDB>().ConfigureAwait(false);
            await connection.CreateTableAsync<SucursalDB>().ConfigureAwait(false);
            await connection.CreateTableAsync<Maestro>().ConfigureAwait(false);
            await connection.CreateTableAsync<Usuario>().ConfigureAwait(false);
            await connection.CreateTableAsync<AreasInspeccion>().ConfigureAwait(false);
            await connection.CreateTableAsync<TipoFotografia>().ConfigureAwait(false);
            await connection.CreateTableAsync<FotografiaDB>().ConfigureAwait(false);
            await connection.CreateTableAsync<FotografiaLocal>().ConfigureAwait(false);
            
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

        public async Task<List<ItemsdetalleinspeccionDB>> GetAllItemsDetalle()
        {
            var query = await this.connection.QueryAsync<ItemsdetalleinspeccionDB>("select * from [ItemsdetalleinspeccionDB]");

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


        public async Task<List<ItemsAreasInspeccionACC>> GetItemsInspeccion()
        {
            var query = await this.connection.QueryAsync<ItemsAreasInspeccionDB>("select * from [ItemsAreasInspeccionDB]");

            var array = query.ToArray();
            var list = array.Select(p => new ItemsAreasInspeccionACC
            {
                ITINS_ID = p.ITINS_ID,
                ITINS_AINSP_ID = p.ITINS_AINSP_ID,
                ITINS_DESCRIPCION = p.ITINS_DESCRIPCION,
                ITINS_CONDICION = p.ITINS_CONDICION,
                ITINS_DESHABILITAR = p.ITINS_DESHABILITAR,
                ITINS_REQUIERE_FOTO = p.ITINS_REQUIERE_FOTO,
                ITINS_ORDEN_APP = p.ITINS_ORDEN_APP,
                ITINS_ACTIVO = p.ITINS_ACTIVO,
                ITINS_STATE_ACTIVO = false,
                ITINS_IS_LOCKED = false
            }).ToList();

            return list;
        }

        public async Task<List<ItemsAreasInspeccionDB>> GetItemsInspeccionDB()
        {
            var query = await this.connection.QueryAsync<ItemsAreasInspeccionDB>("select * from [ItemsAreasInspeccionDB]");

            return query;
        }

        public async Task<List<ItemsdetalleinspeccionDB>> GetDetallesInspeccion()
        {
            var query = await this.connection.QueryAsync<ItemsdetalleinspeccionDB>("select * from [ItemsdetalleinspeccionDB]");

            return query;
        }
        public async Task<List<Fotografia>> GetImagenes()
        {
            var imgDB = await this.connection.QueryAsync<FotografiaDB>("select * from [FotografiaDB]");
            var imgType = await this.connection.QueryAsync<TipoFotografia>("select * from [TipoFotografia]");

            var array = imgDB.ToArray();
            var list = array.Select(p => new Fotografia
            {
                FOTO_ID = p.FOTO_ID,
                FOTO_TIPOF_ID = p.FOTO_TIPOF_ID,
                FOTO_DESCRIPCION = p.FOTO_DESCRIPCION,
                FOTO_REQUIERE_MARCO = p.FOTO_REQUIERE_MARCO,
                //FOTO_MARCO = 
                FOTO_ACTIVO = p.FOTO_ACTIVO,
                FOTO_OBLIGATORIA = p.FOTO_OBLIGATORIA,
                CLCAR_TIPO_FOTOGRAFIA = imgType.Where(a => a.TIPOF_ID == p.FOTO_TIPOF_ID).FirstOrDefault()

            }).ToList();

            return list;
        }

        public async Task<List<FotografiaDB>> GetImagenesDB()
        {
            var imgDB = await this.connection.QueryAsync<FotografiaDB>("select * from [FotografiaDB]");
            
            return imgDB;
        }

        public async Task<List<TipoFotografia>> GetTipoImagenes()
        {
            var imgDB = await this.connection.QueryAsync<TipoFotografia>("select * from [TipoFotografia]");

            return imgDB;
        }

        public async Task<List<FotografiaLocal>> GetLocalImagenes()
        {
            var imgDB = await this.connection.QueryAsync<FotografiaLocal>("select * from [FotografiaLocal]");

            return imgDB;
        }




        #endregion


    }
}
