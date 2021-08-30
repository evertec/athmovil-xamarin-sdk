using ATHMovil.Purchase.Model.Config;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TestApp.Models;
using Xamarin.Forms;
using ATHMovil.Purchase.UI;

namespace TestApp.ViewModels
{
    public class ConfigViewModel : ContentPage
    {

        public ObservableCollection<string> GetListEnviroment {
            get
            {
                List<string> listEnvi = new List<string> {
                    ATHMovilTarget.Production.ToString()
                };
                
                return new ObservableCollection<string>(listEnvi);
            }
        }

        public ObservableCollection<string> GetListTheme
        {
            get
            {
                List<string> listEnvi = new List<string> {
                    Theme.Classic.ToString(),
                    Theme.Light.ToString(),
                    Theme.Dark.ToString()
                };

                return new ObservableCollection<string>(listEnvi);
            }
        }

        public string SelectedTheme
        {
            get => Global.Instance().Theme;
            set
            {
                if (value == null)
                {
                    return;
                }

                Global.Instance().Theme = value;
                GlobalStorage.ShareInstance.Value.SelectedTheme = value;
            } 
        }

        public string SelectedEnviroment
        {
            get => Global.Instance().Environment;
            set
            {
                if (value == null)
                {
                    return;
                }

                Global.Instance().Environment = value;
                GlobalStorage.ShareInstance.Value.SelectedEnviroment = value;
            } 
        }

        public string Token
        {
            get => Global.Instance().Token;
            set
            {
                Global.Instance().Token = value;
                GlobalStorage.ShareInstance.Value.Token = value;
            }
        }

        public int TimeOut
        {
            get => Global.Instance().Timeout;
            set
            {
                Global.Instance().Timeout = value;
                GlobalStorage.ShareInstance.Value.TimeOut = TimeOut;
            }
        }

        public double Total
        {
            get => Global.Instance().Total;
            set
            {
                Global.Instance().Total = value;
                GlobalStorage.ShareInstance.Value.Total = value;
            }
        }

        public double SubTotal
        {
            get => Global.Instance().SubtTotal;
            set
            {
                Global.Instance().SubtTotal = value;
                GlobalStorage.ShareInstance.Value.SubTotal = value;
            }
        }

        public double Tax
        {
            get => Global.Instance().Tax;
            set
            {
                Global.Instance().Tax = value;
                GlobalStorage.ShareInstance.Value.Tax = value;
            }
        }

        public string Metadata1
        {
            get => Global.Instance().Metadata1;
            set
            {
                Global.Instance().Metadata1 = value;
                GlobalStorage.ShareInstance.Value.Metadata1 = value;
            }
        }

        public string Metadata2
        {
            get => Global.Instance().Metadata2;
            set
            {
                Global.Instance().Metadata2 = value;
                GlobalStorage.ShareInstance.Value.Metadata2 = value;
            }
        }

        public ConfigViewModel()
        {
        }
    }
 
}