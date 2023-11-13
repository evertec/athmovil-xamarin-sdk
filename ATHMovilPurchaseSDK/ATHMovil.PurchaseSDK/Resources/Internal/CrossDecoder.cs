using System;
using System.Threading;

namespace ATHMovil.Purchase.Decoders
{
    internal class CrossDecoder
    {
        static Lazy<IDecodable> implementation = new Lazy<IDecodable>(() => CreateCodable(), LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        internal static bool IsSupported => implementation.Value == null ? false : true;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        internal static IDecodable Current
        {
            get
            {
                var ret = implementation.Value;
                if (ret == null)
                {
                    return null;
                }
                return ret;
            }
        }

        static IDecodable CreateCodable()
        {
            return new Decoder();
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

    }
}


