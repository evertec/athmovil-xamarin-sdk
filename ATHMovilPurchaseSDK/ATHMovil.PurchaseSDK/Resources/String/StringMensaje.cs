using System.Reflection;
using ATHMovil.Purchase.Extension;

using System.Globalization;

namespace ATHMovil.PurchaseSDK.String
{
	public static class StringMensaje
	{
        
        public static bool isEnglish = CultureInfoExtension.IsEnglish(CultureInfo.CurrentCulture);
        

        public static string GetBusinessErrorTitle() {
            if (isEnglish)
            {
                return "Business is Unavailable";
            }
            else {
                return "Negocio no Disponible";
            }
        }

        public static string GetBusinessErrorMessage()
        {
            if (isEnglish)
            {
                return "This business can\'t receive payments at the moment. Please try again later.";
            }
            else
            {
                return "Este negocio no puede recibir pagos al momento. Por favor, intenta nuevamente mas tarde.";
            }
        }

        public static string GetGenericErrorTitle()
        {
            if (isEnglish)
            {
                return "Something went wrong…";
            }
            else
            {
                return "Algo salió mal…";
            }
        }

        public static string GetGenericErrorMessage()
        {
            if (isEnglish)
            {
                return "Sorry for the inconvenience. Please try again later.";
            }
            else
            {
                return "Lamentamos los inconvenientes. Por favor intenta nuevamente más tarde.";
            }
        }

        public static string GetGenericErrorMessageOldFlow()
        {
            if (isEnglish)
            {
                return "Sorry, we are currently performing maintenance. Please try again later.";
            }
            else
            {
                return "Lo sentimos, estamos realizando mantenimiento. Por favor, intenta más tarde.";
            }
        }
    }
}

