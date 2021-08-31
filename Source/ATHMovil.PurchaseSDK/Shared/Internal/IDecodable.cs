using ATHMovil.Purchase.Model;

namespace ATHMovil.Purchase.Decoders
{
    internal interface IDecodable
    {
        PurchaseResponse Decode(string responseJSON);
        
    }
}
