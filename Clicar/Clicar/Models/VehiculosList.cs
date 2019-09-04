using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    class VehiculosList
    {

        public List<Vehiculo> GetListaVehiculos()
        {
            var listaVehiculos = new List<Vehiculo>();

            listaVehiculos.AddRange(new[] {
                new Vehiculo
                {
                    Modelo = "GSK342 SPARK", Marca = "MAZDA", Color = "NEGRO", HoraInicio = "17:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },
                new Vehiculo
                {
                    Modelo = "GDST40 CX-5", Marca = "CHEVROLET", Color = "PLATEADO", HoraInicio = "13:00 pm", IDVehiculo = "W-20190703-1298", Finalizado = false
                },
                new Vehiculo
                {
                    Modelo = "JRGK85 TUCSON", Marca = "PEUGEOT", Color = "BLANCO", HoraInicio = "10:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },
                new Vehiculo
                {
                    Modelo = "JRGK85 TUCSON", Marca = "PEUGEOT", Color = "AZUL", HoraInicio = "10:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },
                new Vehiculo
                {
                    Modelo = "JRGK85 TUCSON", Marca = "PEUGEOT", Color = "VERDE PALTA", HoraInicio = "10:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },
                new Vehiculo
                {
                    Modelo = "JRGK85 TUCSON", Marca = "PEUGEOT", Color = "AMARILLO", HoraInicio = "10:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },
                new Vehiculo
                {
                    Modelo = "JRGK85 UNO", Marca = "KIA", Color = "AMARILLO", HoraInicio = "10:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },
                new Vehiculo
                {
                    Modelo = "JRGK85 NOVA", Marca = "CHEVY", Color = "AMARILLO", HoraInicio = "10:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },
                new Vehiculo
                {
                    Modelo = "JRGK85 LANCER", Marca = "MITSUBISHI", Color = "AMARILLO", HoraInicio = "10:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },
                new Vehiculo
                {
                    Modelo = "GSK342 SPARK", Marca = "MAZDA", Color = "NEGRO", HoraInicio = "17:00 pm", IDVehiculo = "W-20190703-1297", Finalizado = true
                },


            });

            return listaVehiculos;


        }




    }
}
