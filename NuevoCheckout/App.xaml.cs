
using ATHMovil.Purchase.Model.Manager;
using NuevoCheckout.ViewModels;
using NuevoCheckout.Views;

namespace NuevoCheckout;

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

