using System;
using ATHMovil.Purchase.UI;
using Xamarin.Forms;
using TestApp.Models;

namespace TestApp.Views
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
            PaymentButton.Theme = (Theme)Enum.Parse(typeof(Theme), global.Theme);
        }
    }
}
