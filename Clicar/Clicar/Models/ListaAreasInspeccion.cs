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
                    Nombre = "Manual, llaves y otros", Orden = 1,
                },
                new AreaInspeccion
                {
                    Nombre = "Equipamento", Orden = 2, 
                },
                new AreaInspeccion
                {
                    Nombre = "Interiores", Orden = 4, 
                },
                new AreaInspeccion
                {
                    Nombre = "Motor y caja", Orden = 3, 
                },
                new AreaInspeccion
                {
                    Nombre = "Fotografía interior", Orden = 5, 
                },
                new AreaInspeccion
                {
                    Nombre = "Exteriores", Orden = 6, 
                },
                new AreaInspeccion
                {
                    Nombre = "Fotografía exterior", Orden = 7, 
                },
                new AreaInspeccion
                {
                    Nombre = "Prueba de ruta", Orden = 8, 
                },
            });

            return listaVehiculos;


        }




    }
}
