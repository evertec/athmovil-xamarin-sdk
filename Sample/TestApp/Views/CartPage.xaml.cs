using Rg.Plugins.Popup.Services;
using System;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp.Views
{
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
        }

        async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConfigPage());
        }

        async void OnCustomItemButtonClicked(object sender, EventArgs e)
        {
            CartViewModel viewModel = ((CartViewModel)BindingContext);

            if (viewModel == null)
            {
                return;
            }

            PopupPage itemPopup = new PopupPage()
            {
                BindingContext = new PopUpViewModel(viewModel.CartList)
            };
            
            await PopupNavigation.Instance.PushAsync(itemPopup);
        }

        async void OnGoToCart(Object sender, EventArgs e)
        {
            ReviewPage review = new ReviewPage();
            await Navigation.PushAsync(review);
        }

    }
}