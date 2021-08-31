using System.Collections.Generic;
using ATHMovil.Purchase.Model;
using ATHMovil.Purchase.Model.Config;

namespace TestApp.Models
{
    public class Global
    {
        public double Total { get; set; }
        public double SubtTotal { get; set; }
        public double Tax { get; set; }
        public string Metadata1 { get; set; }
        public string Metadata2 { get; set; }
        public string Token { get; set; }        
        public int Timeout { get; set; }
        public string Theme { get; set; }
        public string Environment { get; set; }

        public List<PurchaseItem> Items { get; set; }

        private static Global _instance = null;

        private Global()
        {
            Total = 1;
            Token = "Dummy";
            Timeout = 600;
            Environment = ATHMovilTarget.Production.ToString();
            Theme = ATHMovil.Purchase.UI.Theme.Classic.ToString();
        }

        static public Global Instance()
        {
            if (_instance == null)
            {
                _instance = new Global();
                _instance.Token = GlobalStorage.ShareInstance.Value.Token;
                _instance.Timeout = GlobalStorage.ShareInstance.Value.TimeOut;
                _instance.Theme = GlobalStorage.ShareInstance.Value.SelectedTheme; 
                _instance.Environment = GlobalStorage.ShareInstance.Value.SelectedEnviroment;
                _instance.Total = GlobalStorage.ShareInstance.Value.Total; 
                _instance.SubtTotal = GlobalStorage.ShareInstance.Value.SubTotal;
                _instance.Tax = GlobalStorage.ShareInstance.Value.Tax;
                _instance.Metadata1 = GlobalStorage.ShareInstance.Value.Metadata1;
                _instance.Metadata2 = GlobalStorage.ShareInstance.Value.Metadata2;
                _instance.Items = GlobalStorage.ShareInstance.Value.Items;

                return _instance;
            }
            
            return _instance;
        }
    }
}

