using System;
using System.Collections.Specialized;
using System.Globalization;
using ATHMovil.Purchase.Model.Config;
using ATHMovil.Purchase.Storage;

namespace ATHMovil.Purchase.Extension
{
    internal static class DoubleExtension
    {
        internal static bool IsLessThanMinimum(this double target)
        {
            if (double.IsNaN(target) || target < 1)
            {
                return true;
            }
            return false;
        }

        internal static bool IsNanOrNegative(this double target)
        {
            if (double.IsNaN(target) || target < 0 )
            {
                return true;
            }
            return false;
        }

        internal static bool IsBetween(this double target, double min, double max)
        {
            return true;
        }
    }

    internal static class IntExtension
    {
        internal static bool IsLessThanMinimum(this int target)
        {
            if (double.IsNaN(target) || target < 1)
            {
                return true;
            }
            return false;
        }
    }

    internal static class CultureInfoExtension
    {
        /// <summary>
        /// Validate if the current culture is English 
        /// </summary>
        /// <param name="target">Current Culture of the application</param>
        /// <returns>Returns true if the current culture is English otherwise false</returns>
        internal static bool IsEnglish(this CultureInfo target)
        {
            if (target.TwoLetterISOLanguageName == "en")
            {
                return true;
            }
            return false;
        }
    }

    internal static class UriExtension
    {
        /// <summary>
        /// Gets from the Uri the query params, every param is in a string Dictionary
        /// </summary>
        /// <param name="uriSource">Current Uri that containts the Uri's param</param>
        /// <returns>Returns a dictionary with the key-value of the parameters</returns>
        internal static StringDictionary GetQueryParams(this Uri uriSource)
        {
            var parameters = uriSource.GetComponents(UriComponents.Query, UriFormat.SafeUnescaped).Split('&');
            StringDictionary parametersKeyValue = new StringDictionary();
            
            foreach (string item in parameters)
            {
                string[] valueKey = item.Split('=');
                if (valueKey.Length == 2)
                {
                    parametersKeyValue.Add(valueKey[0], Uri.UnescapeDataString(valueKey[1]));
                }
            }
            
            return parametersKeyValue;
        }
    }

    internal static class EnviromentExtension {
        /// <summary>
        /// Returns the current target domain for the endpoint, for production or client implementation
        /// should be Production
        /// </summary>
        /// <param name="currentEnviroment">Current enviroment selected in dummy</param>
        /// <returns></returns>
        internal static Uri GetTargetDomain(this ATHMovilTarget currentEnviroment)
        {
            return new Uri("https://www.athmovil.com");
        }

       internal static string GetAWSDomain(this ATHMovilTarget currentEnviroment)
        {
            return "payments.athmovil.com";
        }
    }
}
