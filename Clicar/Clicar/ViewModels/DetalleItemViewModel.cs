using Clicar.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Clicar.ViewModels
{
    public class DetalleItemViewModel : BaseViewModel
    {


        private MainViewModel MainInstance { get; set; }
        private DetallesItem currentItemDetails;
        private ItemsAreasInspeccion currentItem;
        private List<AreasInspeccion> areasInspeccionDB;
        private AreasInspeccion currentArea;
        private ObservableCollection<ItemsdetalleinspeccionDB> itemsDetalle;
        private bool needImage;
        private bool damageInfo;









        public bool NeedImage
        {
            get { return needImage; }
            set { SetValue(ref needImage, value); }
        }
        public bool DamageInfo
        {
            get { return damageInfo; }
            set { SetValue(ref damageInfo, value); }
        }
        public ObservableCollection<ItemsdetalleinspeccionDB> ItemsDetalle
        {
            get { return itemsDetalle; }
            set { SetValue(ref itemsDetalle, value); }
        }
        public List<AreasInspeccion> AreasInspeccionDB
        {
            get { return areasInspeccionDB; }
            set { SetValue(ref areasInspeccionDB, value); }
        }
        public AreasInspeccion CurrentArea
        {
            get { return currentArea; }
            set { SetValue(ref currentArea, value); }
        }
        public List<AreasInspeccion> ListaAreas { get; set; }
        public ItemsAreasInspeccion CurrentItem
        {
            get { return currentItem; }
            set { SetValue(ref currentItem, value); }
        }
        public DetallesItem CurrentItemDetails
        {
            get { return currentItemDetails; }
            set { SetValue(ref currentItemDetails, value); }
        }





        public DetalleItemViewModel()
        {
            MainInstance = MainViewModel.GetInstance();

            GetPickerItems();
        }

        private async void GetPickerItems()
        {
            ItemsDetalle = new ObservableCollection<ItemsdetalleinspeccionDB>(await MainInstance.DataService.GetAllItemsDetalle());
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName.Equals("CurrentItem"))
            {
                switch (CurrentItem.ITINS_CONDICION)
                {
                    case "Esta presente":
                        DamageInfo = false;
                        break;
                    case "Es Condición":
                        DamageInfo = true;
                        break;
                    case "Es daño":
                        DamageInfo = true;
                        break;
                    default:
                        break;
                }

            }


        }





    }
}
