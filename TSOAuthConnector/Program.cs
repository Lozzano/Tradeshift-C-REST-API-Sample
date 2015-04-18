using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using OAuth;

namespace TSOAuthConnector
{

    class Tradeshift
    {
        public string itemCount { get; set; }
    }

    class Program
    {
        static void Main()
        {
            RunAsync().Wait();
        }
        
        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {

                OAuthRequest OAuthclient = new OAuthRequest
                {
                    Method = "GET",
                    Type = OAuthRequestType.RequestToken,
                    SignatureMethod = OAuthSignatureMethod.HmacSha1,
                    ConsumerKey = "OwnAccount",
                    ConsumerSecret = "OwnAccount",
                    Token = "token",
                    TokenSecret = "token-secret",
                    RequestUrl = "https://api-sandbox.tradeshift.com/tradeshift/rest/external/network/connections",
                    Version = "1.0",
                };

                var auth = OAuthclient.GetAuthorizationHeader();
                Console.WriteLine(auth);

                client.BaseAddress = new Uri("https://api-sandbox.tradeshift.com/tradeshift/rest/external/network/connections");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("X-Tradeshift-TenantId", "ts-tennantid");
                client.DefaultRequestHeaders.Add("Authorization", auth);

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    Tradeshift res = await response.Content.ReadAsAsync<Tradeshift>();
                    Console.WriteLine("{0}", res.itemCount);
                }
                else
                {
                    Console.WriteLine("No success response..");
                }

                Console.WriteLine("HTTP Status: {0}", response.StatusCode);
                Console.ReadLine();

            }
        }

    }    

}
