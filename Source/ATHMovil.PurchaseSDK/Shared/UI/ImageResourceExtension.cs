using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ATHMovil.Purchase.UI
{
    [ContentProperty(nameof(Source))]
    public class ImageResource : IMarkupExtension
    {
        public string Source { get; set; }

        public ImageResource()
        {
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            ImageSource imageResource = ImageSource.FromResource(Source, typeof(ImageResource).GetTypeInfo().Assembly);

            return imageResource;
        }
    }
}
