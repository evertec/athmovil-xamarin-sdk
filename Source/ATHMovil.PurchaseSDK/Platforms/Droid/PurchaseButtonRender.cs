using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;
using ATHMovil.Purchase.Renders.Droid;
using ATHMovil.Purchase.UI;
using Java.Lang;
using AndroidX.Core.View;

[assembly: ExportRenderer(typeof(ATHMButton), typeof(PurchaseButtonRender))]
namespace ATHMovil.Purchase.Renders.Droid
{
    
    public sealed class PurchaseButtonRender : ImageButtonRenderer
    {
        public PurchaseButtonRender(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<ImageButton> e)
        {
            base.OnElementChanged(e);

            int scale = Math.Round(2 * Resources.DisplayMetrics.Density);
            Elevation = scale;
            ViewCompat.SetElevation(this, scale);
            OutlineProvider = Android.Views.ViewOutlineProvider.Bounds;
            SetScaleType(ScaleType.FitCenter);
            RequestLayout();
        }

    }
}
