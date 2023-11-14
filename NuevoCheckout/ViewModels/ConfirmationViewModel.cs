using ATHMovil.Purchase.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using NuevoCheckout.Converters;
using Microsoft.Maui.Controls;

using ATHMovil.Purchase.Storage;
using ATHMovil.Purchase.Utils;
using ATHMovil.PurchaseSDK.String;

namespace NuevoCheckout.ViewModels
{
    public class ConfirmationViewModel : INotifyPropertyChanged
    {
        public bool getService = true;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ReviewCell> ReviewCells { get; set; }

        private ReviewCell? _selectedCell { get; set; }
        public ReviewCell? SelectedCell
        {
            get => _selectedCell;
            set
            {
                _selectedCell = value;

                if (value.HasValue && value.Value.TypeCell == ReviewCellType.general)
                {
                    SelectedCell = null;
                }

                PropertyChangedEventArgs args = new PropertyChangedEventArgs(nameof(SelectedCell));
                PropertyChanged.Invoke(this, args);
            }
        }

        public PurchaseResponse Response { get; set; }  

        public ConfirmationViewModel(PurchaseResponse response)
        {
            Response = response;
            ReviewCells = new ObservableCollection<ReviewCell>();

            Task.Run(async () =>
            {
                if (response != null)
                {
                    this.setData(response);
                }
                else
                {
                    ReviewCells.Add(new ReviewCell() { Title = "Status", SubTitle = "CANCELLED" });
                }
            });
        }

        public void setData(PurchaseResponse response) {
            AddPurchaseInfo(response.Info);
            AddPurchaseUser(response.User);
            AddPurchase(response.Purchase);
        }

        public ConfirmationViewModel()
        {
            ReviewCells = new ObservableCollection<ReviewCell>();
        }

        /// <summary>
        /// Add the purchase info into Observable collection, it will add the parameters status, date, reference number and daily Id
        /// </summary>
        /// <param name="info">Purchase info from the payment response</param>
        private void AddPurchaseInfo(PurchaseInfo info)
        {
            try
            {
                ReviewCells.Add(new ReviewCell() { Title = "Status", SubTitle = $"{info.Status.ToString().ToUpper()}" });
                ReviewCells.Add(new ReviewCell() { Title = "Date", SubTitle = info.TransactionDate.ToString() });
                ReviewCells.Add(new ReviewCell() { Title = "Reference Number", SubTitle = info.ReferenceNumber });
                ReviewCells.Add(new ReviewCell() { Title = "Daily Transaction ID", SubTitle = $"{info.DailyTransactionID}" });
            }
            catch (Exception e) {
                Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar los datos de la transacción", "OK", FlowDirection.MatchParent);
            }
            
        }

        /// <summary>
        /// Add the user info into the observable collection, it will add the parameters name, telephone and email
        /// </summary>
        /// <param name="user">ATH Movil user that paid the purchase</param>
        private void AddPurchaseUser(PurchaseUser user)
        {
            try
            {
                ReviewCells.Add(new ReviewCell() { Title = "Name", SubTitle = user.Name });
                ReviewCells.Add(new ReviewCell() { Title = "Phone Number", SubTitle = user.Telephone });
                ReviewCells.Add(new ReviewCell() { Title = "Email", SubTitle = user.Email });
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar los datos del usuario", "OK", FlowDirection.MatchParent);
            }
            
        }

        /// <summary>
        /// Add the purchas info into the observable collection 
        /// </summary>
        /// <param name="purchase">Purchase info</param>
        private void AddPurchase(Purchase purchase)
        {
            try
            {
                PriceConverter priceConverter = new PriceConverter();
                bool hasItems = purchase.Items.Count > 0 && DeviceInfo.Platform == DevicePlatform.iOS;

                ReviewCells.Add(new ReviewCell() { Title = "Total", SubTitle = $"{priceConverter.Convert(purchase.Total)}" });
                ReviewCells.Add(new ReviewCell() { Title = "Tax", SubTitle = $"{priceConverter.Convert(purchase.Tax)}" });
                ReviewCells.Add(new ReviewCell() { Title = "SubTotal", SubTitle = $"{priceConverter.Convert(purchase.SubTotal)}" });
                ReviewCells.Add(new ReviewCell() { Title = "Fee", SubTitle = $"{priceConverter.Convert(purchase.Fee)}" });
                ReviewCells.Add(new ReviewCell() { Title = "Net Amount", SubTitle = $"{priceConverter.Convert(purchase.NetAmount)}" });
                ReviewCells.Add(new ReviewCell() { Title = "Metadata1", SubTitle = $"{purchase.Metadata1}" });
                ReviewCells.Add(new ReviewCell() { Title = "Metadata2", SubTitle = $"{purchase.Metadata2}" });
                ReviewCells.Add(new ReviewCell() { Title = "Items", SubTitle = $"Total: {purchase.Items.Count}", IsVisible = hasItems, TypeCell = ReviewCellType.items });
            }
            catch (Exception e)
            {
                Application.Current.MainPage.DisplayAlert("Error", "No se pudo cargar los datos de la compra", "OK", FlowDirection.MatchParent);
            } 
            
        }

    }

    public struct ReviewCell
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public bool IsVisible { get; set; }
        internal ReviewCellType TypeCell { get; set; }
    }

    internal enum ReviewCellType
    {
        general,
        items
    }
}
