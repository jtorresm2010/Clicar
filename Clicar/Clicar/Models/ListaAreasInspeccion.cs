using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    class ListaAreasInspeccion
    {
        public List<AreaInspeccion> GetListaAreas()
        {
            var listaVehiculos = new List<AreaInspeccion>();

            listaVehiculos.AddRange(new[] {
                new AreaInspeccion
                {
                    Nombre = "Manual, llaves y otros", Orden = 1, IsImage = false
                },
                new AreaInspeccion
                {
                    Nombre = "Equipamento", Orden = 2, IsImage = false
                },
                new AreaInspeccion
                {
                    Nombre = "Interiores", Orden = 4, IsImage = false
                },
                new AreaInspeccion
                {
                    Nombre = "Motor y caja", Orden = 3, IsImage = false
                },
                new AreaInspeccion
                {
                    Nombre = "Fotografía interior", Orden = 5, IsImage = true
                },
                new AreaInspeccion
                {
                    Nombre = "Exteriores", Orden = 6, IsImage = false
                },
                new AreaInspeccion
                {
                    Nombre = "Fotografía exterior", Orden = 7, IsImage = true
                },
                new AreaInspeccion
                {
                    Nombre = "Prueba de ruta", Orden = 8, IsImage = false
                },
            });

            return listaVehiculos;


        }




    }
}
