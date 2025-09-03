using System.ComponentModel;
using Microsoft.Maui;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Config;
using ATHMovil.Purchase.Model.Manager;
using ATHMovil.Purchase.Utils;
using System.Threading.Tasks;
using NuevoCheckout.Views;
using NuevoCheckout.Models;
using System;

namespace NuevoCheckout.ViewModels
{
    public class ReviewViewModel
    {

        public Command OnPayATHMovil { get; set; }
        public INavigation Navigation { get; set; }
        public PurchaseResponse response { get; set; }

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
                    Items = global.Items,
                    PhoneNumber = global.Phone
                };

                PurchaseHandler handler = new(OnResponseCompleted, OnResponseCancelled, OnResponseExpired, OnException);

                PurchaseRequest request = new(purchase, publicToken, callback);
                request.Pay(handler, global.Timeout);
            });
        }

        private void OnResponseCompleted(PurchaseResponse response) {
            getServiceAuthorization(response);
        }

        private void OnResponseCancelled(PurchaseResponse response) {
            _ = ShowConfirmationPageAsync(response);
        }

        private void OnResponseExpired(PurchaseResponse response) {
            _ = ShowConfirmationPageAsync(response);
        }

        private void OnResponseFailed(PurchaseResponse response)
        {
            _ = ShowConfirmationPageAsync(response);
        }

        private void OnException(PurchaseException exception) {
            _ = Application.Current.MainPage.DisplayAlert(exception.FailureReason, exception.Description, "OK", FlowDirection.MatchParent);
        }

        private async void getServiceAuthorization(PurchaseResponse response) {

            this.response = response;

            await Task.Delay(1000);

            if (!Global.Instance().Token.ToLower().Equals("dummy")){
                AuthorizationServices service = new AuthorizationServices();
                PurchaseResponse authorizationObject = await service.AuthorizationServicesCall(response);
                _ = ShowConfirmationPageAsync(authorizationObject);
            }else{
                _ = ShowConfirmationPageAsync(response);
            }
        }

        private async Task ShowConfirmationPageAsync(PurchaseResponse response){

            this.response = response;

            

            ConfirmationPage confirmation = new ConfirmationPage
            {
                BindingContext = new ConfirmationViewModel(response)
            };

            await Task.Delay(500);

            NavigationPage navigation = new NavigationPage(confirmation);
            await Application.Current.MainPage.Navigation.PushModalAsync(navigation);
        }
    }
}


