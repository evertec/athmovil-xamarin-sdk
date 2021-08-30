
using ATHMovil.Purchase.Model.Manager;
using TestApp.ViewModels;
using TestApp.Views;
using Xamarin.Forms;

namespace TestApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CartPage cartPage = new CartPage()
            {
                BindingContext = new CartViewModel()
            };
            MainPage = new NavigationPage(cartPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            PurchaseManager.OnResume();
        }
    }
}
