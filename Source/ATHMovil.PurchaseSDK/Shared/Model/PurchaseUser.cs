
namespace ATHMovil.Purchase.Model
{
    public sealed class PurchaseUser
    {
        public string Name { get; private set; }
        public string Telephone { get; private set; }
        public string Email { get; private set; }

        internal PurchaseUser()
        {
            Name = "";
            Telephone = "";
            Email = "";
        }

        internal PurchaseUser(string name, string telephone, string email)
        {
            Name = name;
            Telephone = telephone;
            Email = email;
        }
    }
}
