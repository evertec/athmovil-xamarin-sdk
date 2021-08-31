using System;
using System.Threading;

namespace ATHMovil.Purchase.Openers
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CrossOpener
    {
        static Lazy<IOpener> implementation = new Lazy<IOpener>(() => CreateOpener(), LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets if the plugin is supported on the current platform.
        /// </summary>
        internal static bool IsSupported => implementation.Value == null ? false : true;

        /// <summary>
        /// Current plugin implementation to use
        /// </summary>
        internal static IOpener Current
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

        static IOpener CreateOpener()
        {
#if NETSTANDARD2_0
            return null;
#else
            return new Opener();
#endif
        }

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

    }
}