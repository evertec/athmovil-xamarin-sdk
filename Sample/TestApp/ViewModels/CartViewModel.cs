
using System.Collections.ObjectModel;
using Xamarin.Forms;
using ATHMovil.Purchase.Model;
using TestApp.Models;
using System.Collections.Generic;

namespace TestApp.ViewModels
{
    public partial class CartViewModel
    {

        public Command AddDefaultButtonCommand { get; private set; }
        public Command DeleteCommand { get; set; }
        public ObservableCollection<PurchaseItem> CartList { get; set; }
        public Global global = Global.Instance();

        public CartViewModel()
        {
            CartList = new ObservableCollection<PurchaseItem>(global.Items);

            AddDefaultButtonCommand = new Command(async =>
            {
                PurchaseItem itemToAdd = new PurchaseItem("Item", 1, 1.00) { Description = "Description", Metadata = "Metadata" };
                CartList.Add(itemToAdd);
                global.Items.Add(itemToAdd);
                GlobalStorage.ShareInstance.Value.Items = new List<PurchaseItem>(CartList);
            });

            DeleteCommand = new Command(async =>
            {
                CartList.Clear();
                global.Items = new List<PurchaseItem>();
                GlobalStorage.ShareInstance.Value.Items = new List<PurchaseItem>();
            });

        }
    }
}