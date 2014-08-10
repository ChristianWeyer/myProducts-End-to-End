using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace myProducts.WarmupHelper
{
    class Program
    {
        static void Main()
        {
            ServicePointManager.DefaultConnectionLimit = Int32.MaxValue;
            // TODO: no, not really! :(
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

            CallServices();
            Console.ReadLine();
        }

        private static async void CallServices()
        {
            var localClient = new HttpClient { BaseAddress = new Uri("https://windows8vm") };
            var localToken = GetToken("https://windows8vmtoken");
            localClient.SetBearerToken(localToken.AccessToken);

            var cloudClient = new HttpClient { BaseAddress = new Uri("https://demo.christianweyer.net/") };
            var cloudToken = GetToken("https://demo.christianweyer.net/token");
            cloudClient.SetBearerToken(cloudToken.AccessToken);

            var tasks = new List<Task>();
            while (true)
            {
                try
                {
                    Console.WriteLine("Calling 'app'");
                    tasks.Add(localClient.GetAsync("app"));
                    tasks.Add(cloudClient.GetAsync("app"));

                    Console.WriteLine("Calling 'personalization'");
                    tasks.Add(localClient.GetAsync("api/personalization"));
                    tasks.Add(cloudClient.GetAsync("api/personalization"));

                    Console.WriteLine("Calling 'articles'");
                    tasks.Add(localClient.GetAsync("api/articles"));
                    tasks.Add(cloudClient.GetAsync("api/articles"));

                    Console.WriteLine("Calling 'images'");
                    tasks.Add(localClient.GetAsync("api/images"));
                    tasks.Add(cloudClient.GetAsync("api/images"));

                    Console.WriteLine("Calling 'statistics'");
                    tasks.Add(localClient.GetAsync("api/statistics/distribution"));
                    tasks.Add(cloudClient.GetAsync("api/statistics/distribution"));

                    await Task.WhenAll(tasks);
                    await Task.Delay(10000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("EXCEPTION: " + ex.Message);
                    //throw;
                }
            }
        }

        private static TokenResponse GetToken(string tokenUrl)
        {
            var client = new OAuth2Client(
                new Uri(tokenUrl));

            return client.RequestResourceOwnerPasswordAsync("cw", "cw").Result;
        }
    }
}
