using Foundation;
using UIKit;
using ATHMovil.Purchase.Model.Manager;

namespace NuevoCheckout;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    [Export("application:openURL:options:")]
    public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
    {
        return PurchaseManager.tryComplete(new Uri(url.AbsoluteString));
    }
}

