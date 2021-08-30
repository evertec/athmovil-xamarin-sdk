using System.ComponentModel;
using Xamarin.Forms;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Config;
using ATHMovil.Purchase.Model.Manager;
using System.Threading.Tasks;
using TestApp.Views;
using TestApp.Models;
using System;

namespace TestApp.ViewModels
{
    public class ReviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Command OnPayATHMovil { get; set; }
        public INavigation Navigation { get; set; }

        
        public ReviewViewModel()
        {
            Global global = Global.Instance();
            PurchaseManager.SharedInstance.TargetEnviroment = (ATHMovilTarget)Enum.Parse(typeof(ATHMovilTarget), global.Environment);

            OnPayATHMovil = new Command(() =>
            { 
                Business publicToken = new Business(global.Token);
                UriCallBack callback = new UriCallBack("test-app");
                
                Purchase purchase = new Purchase(global.Total)
                {
                    SubTotal = global.SubtTotal,
                    Tax = global.Tax,
                    Metadata1 = global.Metadata1,
                    Metadata2 = global.Metadata2,
                    Items = global.Items
                };

                PurchaseHandler handler = new PurchaseHandler(OnResponseCompleted, OnResponseCancelled, OnResponseExpired, OnException);

                PurchaseRequest request = new PurchaseRequest(purchase, publicToken, callback);
                request.Pay(handler, global.Timeout);
            });
        }

        private void OnResponseCompleted(PurchaseResponse response) {
            _ = ShowConfirmationPageAsync(response);
        }

        private void OnResponseCancelled(PurchaseResponse response) {
            _ = ShowConfirmationPageAsync(response);
        }

        private void OnResponseExpired(PurchaseResponse response) {
            _ = ShowConfirmationPageAsync(response);
        }

        private void OnException(PurchaseException exception) {
            _ = Application.Current.MainPage.DisplayAlert(exception.FailureReason, exception.Description, "OK", FlowDirection.MatchParent);
        }

        private async Task ShowConfirmationPageAsync(PurchaseResponse response)
        {
            ConfirmationPage confirmation = new ConfirmationPage
            {
                BindingContext = new ConfirmationViewModel(response)
            };
            NavigationPage navigation = new NavigationPage(confirmation);
            await Application.Current.MainPage.Navigation.PushModalAsync(navigation);
        }
    }
}


