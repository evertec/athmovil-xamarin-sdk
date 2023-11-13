using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using ATHMovil.Purchase;

namespace NuevoCheckout;

[Activity(Theme = "@style/Maui.SplashTheme")]
public class MainActivity : MauiAppCompatActivity
{

	private App app { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);

        }

}

[Activity(Name = "com.companyname.testapp.ResponseActivity")]
public class ResponseActivity : PurchaseResponseActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
    }
}

