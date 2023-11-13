
namespace NuevoCheckout.Views
{
 
    public partial class PopupPageModal : ContentPage  
    {

        public PopupPageModal()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.BackgroundColor = Color.FromRgba(149, 145, 144, 1);
            }
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        public async void CloseAllPopup()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CloseAllPopup();
        }
    }
}