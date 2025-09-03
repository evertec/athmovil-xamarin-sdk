using ATHMovil.Purchase.UI;
using NuevoCheckout.Models;
using Microsoft.Maui.Controls;

namespace NuevoCheckout.Views
{
    public partial class ReviewPage : ContentPage
    {
        public ReviewPage()
        {
            InitializeComponent();

            Global global = Global.Instance();
            token.Text = global.Token;
            timeout.Text = global.Timeout.ToString();
            subtotal.Text = global.SubtTotal.ToString();
            tax.Text = global.Tax.ToString();
            total.Text = global.Total.ToString();
            metadata1.Text = global.Metadata1;
            metadata2.Text = global.Metadata2;
            items.Text = $"{ global.Items.Count }";

            phone.Text = $"{global.Phone}";

            PaymentButton.Theme = (Theme)Enum.Parse(typeof(Theme), global.Theme);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
