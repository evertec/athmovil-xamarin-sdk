using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Config;
namespace ATHMovil.Purchase.Openers
{
    public enum OpenerResult
    {
        ATHMovilPersonal,
        ApplicationsStore,
        NotSupportedPlatform,
        Error
    }

    internal interface IOpener
    {
        OpenerResult OpenATHMovil(PurchaseRequest request, ATHMovilTarget target);
    }
}
