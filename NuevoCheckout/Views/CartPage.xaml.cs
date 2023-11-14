using System;
using NuevoCheckout.ViewModels;
using Microsoft.Maui;

namespace NuevoCheckout.Views
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

        async void OnCustomItemButtonClickedAsync(object sender, EventArgs e)
        {
            CartViewModel viewModel = ((CartViewModel)BindingContext);

            if (viewModel == null)
            {
                return;
            }

            PopupPageModal itemPopup = new PopupPageModal()
            {
                BindingContext = new PopUpViewModel(viewModel.CartList)
            };
            await Navigation.PushModalAsync(itemPopup);
        }

        async void OnGoToCart(Object sender, EventArgs e)
        {
            ReviewPage review = new ReviewPage();
            await Navigation.PushAsync(review);
        }

    }
}