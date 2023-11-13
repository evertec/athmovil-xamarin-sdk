using System;
namespace ATHMovil.Purchase.Utils
{
	public static class LoadingView
	{
		
        public static void ShowLoadingOverlay(){

            var loadingOverlay = new StackLayout{

                BackgroundColor = Color.FromRgba(0, 0, 0, 0.3),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new ActivityIndicator
                    {
                        Color = Color.FromRgba(255, 255, 255, 1.0),
                        IsRunning = true,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };

            var modalPage = new ContentPage {
                BackgroundColor = Color.FromRgba(0, 0, 0, 0.3),
                Content = loadingOverlay
            };
            Application.Current.MainPage.Navigation.PushModalAsync(modalPage);
        }

        public static void HideLoadingOverlay(){
            Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}

