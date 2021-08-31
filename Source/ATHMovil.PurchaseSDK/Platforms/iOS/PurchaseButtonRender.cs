using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ATHMovil.Purchase.Renders.iOS;
using CoreGraphics;
using ATHMovil.Purchase.UI;

[assembly: ExportRenderer(typeof(ATHMButton), typeof(PurchaseButtonRender))]
namespace ATHMovil.Purchase.Renders.iOS
{
    public sealed class PurchaseButtonRender : ImageButtonRenderer
    {
        public PurchaseButtonRender() : base()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ImageButton> e)
        {
            base.OnElementChanged(e);
    
            if (Control != null)
            {
                Layer.ShadowColor = Color.FromRgb(0,0,0).MultiplyAlpha(0.25).ToCGColor();
                Layer.ShadowOffset = new CGSize(0.0, 2.0);
                Layer.ShadowRadius = 2;
                Layer.ShadowOpacity = 1;
                Layer.CornerRadius = 4;
                e.NewElement.BorderWidth = 0;
            }
        }
    }
}
