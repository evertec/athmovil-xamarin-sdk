using System;
using NuevoCheckout.Converters;
using NuevoCheckout.Models;
using NuevoCheckout.ViewModels;
using Microsoft.Maui;
using NuevoCheckout.Extensions;

namespace NuevoCheckout.Views
{
    public partial class ConfigPage : ContentPage
    {
        private Global global = Global.Instance();
        private ConfigViewModel vm = new ConfigViewModel();
        private PriceConverter converter = new PriceConverter();

        public ConfigPage()
        {
            InitializeComponent();

            PickerEnviroment.SelectedItem = global.Environment;
            PickerFlow.SelectedItem = global.Flow;
            PickerTheme.SelectedItem = global.Theme;
        }

        async void OnClickToken(Object sender, EventArgs e)
        {
            Label pressedLabel = (Label)sender;
            string labelId = pressedLabel.StyleId;
            string promptInput;
            switch (labelId) {
                case "tokenEntryBox":
                    promptInput = await DisplayPromptAsync("Token", "Enter the business token", "OK", "CANCEL", "Business Token", 40, Keyboard.Text);
                    vm.Token = promptInput;
                    tokenEntryBox.Text = promptInput;
                    break;
                case "timeoutEntryBox":
                    promptInput = await DisplayPromptAsync("Timeout", "Enter the transaction timeout", "OK", "CANCEL", "60", 40, Keyboard.Numeric);
                    int timeOutValue = promptInput.CastInt();
                    vm.TimeOut = timeOutValue;
                    timeoutEntryBox.Text = $"{timeOutValue}";
                    break;
                case "totalEntryBox":
                    promptInput = await DisplayPromptAsync("Total", "Enter the payment total", "OK", "CANCEL", "1.00", 40, Keyboard.Numeric);
                    double selectedTotal = promptInput.CastDouble();
                    vm.Total = selectedTotal;
                    totalEntryBox.Text = converter.Convert(selectedTotal);
                    break;
                case "subTotalEntryBox":
                    promptInput = await DisplayPromptAsync("Subtotal", "Enter the payment subtotal", "OK", "CANCEL", "0.00", 40, Keyboard.Numeric);
                    double selectedSubTotal = promptInput.CastDouble();
                    vm.SubTotal = selectedSubTotal;
                    subTotalEntryBox.Text = converter.Convert(selectedSubTotal); ;
                    break;
                case "taxEntryBox":
                    promptInput = await DisplayPromptAsync("Tax", "Enter the payment tax", "OK", "CANCEL", "0.00", 40, Keyboard.Numeric);
                    double selectedTax = promptInput.CastDouble();
                    vm.Tax = selectedTax;
                    taxEntryBox.Text = converter.Convert(selectedTax);
                    break;
                case "metadata1EntryBox":
                    promptInput = await DisplayPromptAsync("Metadata1", "Enter the metadata 1", "OK", "CANCEL", "Metadata1", 40, Keyboard.Text);
                    vm.Metadata1 = promptInput;
                    metadata1EntryBox.Text = promptInput;
                    break;
                case "metadata2EntryBox":
                    promptInput = await DisplayPromptAsync("Metadata2", "Enter the metadata 2", "OK", "CANCEL", "Metadata2", 40, Keyboard.Text);
                    vm.Metadata2 = promptInput;
                    metadata2EntryBox.Text = promptInput;
                    break;
                case "phoneEntryBox":
                    promptInput = await DisplayPromptAsync("Phone Number", "Enter the phone number", "OK", "CANCEL", "Phone", 15, Keyboard.Text);
                    vm.Phone = promptInput;
                    phoneEntryBox.Text = promptInput;
                    break;
            }
        }

    }
}
