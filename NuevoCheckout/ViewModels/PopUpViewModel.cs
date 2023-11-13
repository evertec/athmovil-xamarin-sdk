using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ATHMovil.Purchase.Model;
using NuevoCheckout.Extensions;
using Microsoft.Maui;
using Newtonsoft.Json;
using NuevoCheckout.Models;

namespace NuevoCheckout.ViewModels
{
    public class PopUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string name = "";
        private double price = 0.0;
        private string description = "";
        private int quantity = 0;
        private string metadata = "";
        private ObservableCollection<PurchaseItem> listItems;
        public Command AddItem { get; private set; }

        public string Name
        {
            get => name;
            set => name = value;

        }

        public string Price
        {
            get => price == 0 ? string.Empty : $"{ price }";
            set => price = value.CastDouble();
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string Quantity
        {
            get => quantity == 0 ? string.Empty : $"{ quantity }";
            set => quantity = value.CastInt();
        }

        public string Metadata
        {
            get => metadata; 
            set => metadata = value;
        }

      
        public PopUpViewModel(ObservableCollection<PurchaseItem> collection)
        {
            listItems = collection;

            AddItem = new Command(async =>
            {
                PurchaseItem itemToAdd = new PurchaseItem(name, quantity, price)
                {
                    Metadata = metadata,
                    Description = description
                };
                listItems.Add(itemToAdd);

                GlobalStorage.ShareInstance.Value.Items = new List<PurchaseItem>(listItems);
                Global.Instance().Items.Add(itemToAdd);
            });
        }

        public PopUpViewModel()
        {

        }
        
    }
}
