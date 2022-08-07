using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECOCEANO.Services
{
    public class PayPalConfiguration
    {

        public readonly static string ClientId;
        public readonly static string ClientSecret;

        static PayPalConfiguration()
        {
            var config = GetConfig();
            ClientId = config["clientId"];
            ClientSecret = config["clientSecret"];
        }
        public static Dictionary<string, string> GetConfig()
        {
            return new Dictionary<string, string>
            {
                {"clientId", "ARzIENY5FR_GnNhlHPppntpW6qB90W8BpT76Bz7zcWe_Scr3oXDt4uZG7HMQO_p0FYFOAc6zcqe3K-3Z" },
                {"clientSecret", "ENlk4Lq27gi7p9ptjHP0N-xQvxl4ILfBjAYUSqfIRJ4kyo3kPlEY7vPQX4uHuReBkufkJ5aJmwfaXb-b" }
            };
        }
        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential
                (ClientId, ClientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }
        public static APIContext GetAPIContext(string accessToken = "")
        {
            var apiContext = new APIContext(string.IsNullOrEmpty(accessToken) ?
                 GetAccessToken() : accessToken);
            apiContext.Config = GetConfig();

            return apiContext;
        }

    }
}
