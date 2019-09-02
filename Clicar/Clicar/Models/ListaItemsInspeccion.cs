using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    public class ListaItemsInspeccion
    {

        public List<ItemInspeccion> GetListaItems()
        {
            var listaItems = new List<ItemInspeccion>();

            listaItems.AddRange(new[] 
            {

                //Area 1
                new ItemInspeccion
                {
                    Nombre = "Llave original con chip", Area="1", Deshabilitable= false, RequiereFoto = true, Tipo = "2"
                },
                new ItemInspeccion
                {
                    Nombre = "Segunda llave original con chip", Area="1", Deshabilitable= false, RequiereFoto = true, Tipo = "2"
                },
                new ItemInspeccion
                {
                    Nombre = "Manual propietario", Area="1", Deshabilitable= false, RequiereFoto = true, Tipo = "2"
                },
                new ItemInspeccion
                {
                    Nombre = "Libro de mantención", Area="1", Deshabilitable= false, RequiereFoto = true, Tipo = "2"
                },
                new ItemInspeccion
                {
                    Nombre = "Placas patentes", Area="1", Deshabilitable= false, RequiereFoto = true, Tipo = "2"
                },

                //Area 2
                new ItemInspeccion
                {
                    Nombre = "Aire acondicionado", Area="2", Deshabilitable= true, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Climatizador", Area="2", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "ABS", Area="2", Deshabilitable= true, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Airbags laterales", Area="2", Deshabilitable= true, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Cinturones pirotécnicos", Area="2", Deshabilitable= true, RequiereFoto = true, Tipo = "1"
                },
                //Area 3
                new ItemInspeccion
                {
                    Nombre = "Motor", Area="3", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Nivel de aceite", Area="3", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Radiador", Area="3", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Tapa del combustible", Area="3", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Pérdidas de agua", Area="3", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                //Area 4
                new ItemInspeccion
                {
                    Nombre = "Volante", Area="4", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Asientos delanteros", Area="4", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Asientos traseros", Area="4", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Piso, alfombra", Area="4", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Espejo retrovisor interior", Area="4", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                //Area 5
                new ItemInspeccion
                {
                    Nombre = "Tablero encendido", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Cabina general", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Llaves y documentos", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Asientos delanteros", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Asientos traseros", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Maleta abierta interior pickup", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Ruedas de repuestos", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Opcional 1", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Opcional 2", Area="5", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                //Area 6
                new ItemInspeccion
                {
                    Nombre = "Parachoques delanteros", Area="6", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Frontal interior", Area="6", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Capot", Area="6", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Tapabarro derecho", Area="6", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Costado Tras. Der.", Area="6", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                //Area 7
                new ItemInspeccion
                {
                    Nombre = "Frontal", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Lateral izquierdo", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Lateral derecho", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Techo", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Capot", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },
                new ItemInspeccion
                {
                    Nombre = "Tapa Maleta", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Prabrisas delantero", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Luneta trasera", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Rueda delantera izquierda", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Rueda delantera derecha", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Rueda trasera derecha", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Rueda trasera izquierda", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Debajo frontal", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Debajo izquierdo frontal", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                },

                new ItemInspeccion
                {
                    Nombre = "Debajo derecho frontal", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                new ItemInspeccion
                {
                    Nombre = "Debajo trasero", Area="7", Deshabilitable= false, RequiereFoto = true, Tipo = "3"
                    },
                
                
                //Area 8
                new ItemInspeccion
                {
                    Nombre = "Arranque", Area="8", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Aceleración", Area="8", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Vibraciones", Area="8", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Alineación", Area="8", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },
                new ItemInspeccion
                {
                    Nombre = "Suspensión", Area="8", Deshabilitable= false, RequiereFoto = true, Tipo = "1"
                },


            });

            return listaItems;


        }


    }
}
