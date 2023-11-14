using System;
using NuevoCheckout.ViewModels;
using Microsoft.Maui;
namespace NuevoCheckout.Views
{
    public partial class ConfirmationPage : ContentPage
    {
        public ConfirmationPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ConfirmationViewModel)BindingContext).SelectedCell = null;
        }

        void Closed_Clicked(Object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        void CollectionView_SelectionChanged(Object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count <= 0 || !(e.CurrentSelection[0] is ReviewCell) || ((ReviewCell)e.CurrentSelection[0]).TypeCell == ReviewCellType.general)
            {
                return;
            }

            ConfirmationViewModel viewModel = ((ConfirmationViewModel)BindingContext);
            ConfirmationItemsPage itemsPage = new ConfirmationItemsPage
            {
                BindingContext = new ConfirmationItemsViewModel(viewModel.Response.Purchase.Items)
            };

            Navigation.PushAsync(itemsPage);
        }
    }
}
