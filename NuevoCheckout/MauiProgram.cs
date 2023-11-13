using Microsoft.Maui.Controls.Compatibility.Hosting;
using ATHMovil.Purchase.UI;

namespace NuevoCheckout;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
        .UseMauiCompatibility()
        .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });
        
        return builder.Build();
	}
}

