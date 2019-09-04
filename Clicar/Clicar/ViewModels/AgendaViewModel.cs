using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace Clicar.ViewModels
{
    public class AgendaViewModel : BaseViewModel
    {

        private ObservableCollection<Vehiculo> listaVehiculos;
        public ObservableCollection<Vehiculo> ListaVehiculos
        {
            get { return this.listaVehiculos; }
            set { this.SetValue(ref this.listaVehiculos, value); }
        }

        private int PendienteCount;
        private double pendientesHeight;
        public double PendientesHeight
        {
            get { return pendientesHeight; }
            set { SetValue(ref pendientesHeight, value); }
        }

        private bool listLoading;
        public bool ListLoading
        {
            get { return listLoading; }
            set { SetValue(ref listLoading, value); }
        }


        private int ListCount = 1;
        private double rowHeight = 110;
        public double RowHeight
        {
            get { return rowHeight; }
            set { SetValue(ref rowHeight, value); }
        }

        public AgendaViewModel()
        {
            InicializarLista();
        }

        private void InicializarLista()
        {
            ListaVehiculos = new ObservableCollection<Vehiculo>(new VehiculosList().GetListaVehiculos());
            PendienteCount = listaVehiculos.Count;
            PendientesHeight = listaVehiculos.Count * RowHeight;
        }

        public bool IsLastItem()
        {
            if (PendienteCount == ListCount)
            {
                ListCount = 1;
                return true;
            }
            else
            {
                ListCount++;
                return false;
            }
        }
    }
}
