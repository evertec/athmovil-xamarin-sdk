using AndroidX.AppCompat.App;
using Android.OS;
using ATHMovil.Purchase.Model.Manager;
namespace ATHMovil.Purchase
{
    public class PurchaseResponseActivity : AppCompatActivity
    {
        private const string DroidResponseParameter = "paymentResult";
        internal bool IsActivityFromATHMovil { get; set; }
        protected bool IsFromATHMovil { get => IsActivityFromATHMovil; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string parameterValue = Intent.GetStringExtra(DroidResponseParameter);
            IsActivityFromATHMovil = PurchaseManager.tryComplete(parameterValue);
            Finish();
        }
    }
}
