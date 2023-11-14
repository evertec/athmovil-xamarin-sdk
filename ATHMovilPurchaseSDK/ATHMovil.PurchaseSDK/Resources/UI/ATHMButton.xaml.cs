using System.Reflection;
using ATHMovil.Purchase.Extension;
using Microsoft.Maui.Controls;

namespace ATHMovil.Purchase.UI
{
    public enum Theme
    {
        Classic,
        Light,
        Dark
    }

    public sealed partial class ATHMButton : ImageButton
    {
        private static readonly BindableProperty ThemeProperty = BindableProperty.Create(nameof(Theme), typeof(Theme), typeof(ATHMButton), Theme.Classic, BindingMode.TwoWay);
        private static readonly BindableProperty LanguageProperty = BindableProperty.Create(nameof(Language), typeof(Language), typeof(ATHMButton), Language.Default, BindingMode.TwoWay);

        public Theme Theme
        {
            get { return (Theme)GetValue(ThemeProperty); }
            set { SetValue(ThemeProperty, value); }
        }

        public Language Language
        {
            get { return (Language)GetValue(LanguageProperty); }
            set { SetValue(LanguageProperty, value); }
        }

        public ATHMButton() : base()
        {
            InitializeComponent();

            Color colorToSet = Theme.BackGroundColor();
            Source = ImageSource.FromFile(Theme.ImageName(Language));
            Aspect = Aspect.AspectFit;
            BackgroundColor = colorToSet;
            var shadow = new Shadow()
            {
                Brush = Brush.DarkGray,
                Offset = new Point(2.0, 4.0),
                Radius = 2,
                Opacity = 1
            };
            this.CornerRadius = 4;
            Shadow = shadow;

            #if ANDROID
                MinimumHeightRequest = 80;
                MaximumHeightRequest = 80;
            #endif

            #if IOS
                MinimumHeightRequest = 50;
                MaximumHeightRequest = 50;
            #endif
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ThemeProperty.PropertyName || propertyName == LanguageProperty.PropertyName)
            {
                Color colorToSet = Theme.BackGroundColor();
                Source = ImageSource.FromFile(Theme.ImageName(Language));
                BackgroundColor = colorToSet;
            }
        }
    }
}
