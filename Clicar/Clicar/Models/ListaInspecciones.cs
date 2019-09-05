using System;
using System.Collections.Generic;
using System.Text;

namespace Clicar.Models
{
    class ListaInspecciones
    {
        public List<Inspeccion> GetListaInspeccion()
        {
            var listaInspeccion = new List<Inspeccion>();

            listaInspeccion.AddRange(new[] {
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-2831354", Patente="QQ1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Anulado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-2100354", Patente="NT1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Completado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-201354", Patente="ZA1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Pendiente"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-21021354", Patente="VD1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Pendiente"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-218354", Patente="FE1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Completado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-21212354", Patente="JY1245", StartTime = "1220", Brand = "CHEVY", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Pendiente"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-21354354", Patente="EF1245", StartTime = "1220", Brand = "CHEVROLET", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Anulado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-211451354", Patente="AB1245", StartTime = "1220", Brand = "MAZDA", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Anulado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-21524354", Patente="AB1225", StartTime = "1220", Brand = "KIA", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Completado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-2341354", Patente="AB2545", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Pendiente"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-2831354", Patente="QQ1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Anulado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-2100354", Patente="NT1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Completado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-201354", Patente="ZA1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Pendiente"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-21021354", Patente="VD1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Pendiente"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-218354", Patente="FE1245", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Completado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-21212354", Patente="JY1245", StartTime = "1220", Brand = "CHEVY", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Pendiente"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-21354354", Patente="EF1245", StartTime = "1220", Brand = "CHEVROLET", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Anulado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-211451354", Patente="AB1245", StartTime = "1220", Brand = "MAZDA", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Anulado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-21524354", Patente="AB1225", StartTime = "1220", Brand = "KIA", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Completado"
                      },
                      new Inspeccion
                      {
                          Num_Inspeccion = "W-2341354", Patente="AB2545", StartTime = "1220", Brand = "PEUGEOT", Model = "208", Color = "Blanco", Year = "2017", Version = "208 Act", Mileage = "120000", Type = "CITI_CAR", Client_Name = "Patricio Alarcon", Client_Rut = "12.968.564-4", Estado = "Pendiente"
                      },
            });

            return listaInspeccion;


        }


    }
}
