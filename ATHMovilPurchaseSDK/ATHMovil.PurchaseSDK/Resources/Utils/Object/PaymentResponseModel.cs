using System;
namespace ATHMovil.Purchase.Utils
{
    public class PaymentResponseModel
    {
        public string status { get; set; }
        public Ecommerce data { get; set; }
        public string message { get; set; }
        public string errorcode { get; set; }

        public string getMessage()
        {
            return message;
        }

        public void setMessage(string message)
        {
            this.message = message;
        }

        public string getErrorcode()
        {
            return errorcode;
        }

        public void setErrorcode(string errorcode)
        {
            this.errorcode = errorcode;
        }

        public string getStatus()
        {
            return status;
        }

        public void setStatus(string status)
        {
            this.status = status;
        }

        public Ecommerce getData()
        {
            return data;
        }

        public void setData(Ecommerce data)
        {
            this.data = data;
        }
    }

    public class Ecommerce {
        public string ecommerceId { get; set; }
        public string auth_token { get; set; }

        public string getEcommerce()
        {
            return ecommerceId;
        }

        public void setEcommerce(string ecommerceId)
        {
            this.ecommerceId = ecommerceId;
        }

        public string getToken()
        {
            return auth_token;
        }

        public void setToken(string auth_token)
        {
            this.auth_token = auth_token;
        }
    }
}

