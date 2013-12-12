using System;
using System.Net.Http;
using System.Threading;

namespace myProducts.WarmupHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var localClient = new HttpClient();
            localClient.BaseAddress = new Uri("https://windows8vm/ngmd/api/");
            localClient.SetBasicAuthentication("cw", "cw");

            var cloudClient = new HttpClient();
            cloudClient.BaseAddress = new Uri("https://demo.christianweyer.net/api/");
            cloudClient.SetBasicAuthentication("cw", "cw");

            while (true)
            {
                Console.WriteLine("Calling 'personalization'");
                localClient.GetAsync("personalization");
                cloudClient.GetAsync("personalization");
                Console.WriteLine("Calling 'articles'");
                localClient.GetAsync("articles");
                cloudClient.GetAsync("articles");
                Console.WriteLine("Calling 'images'");
                localClient.GetAsync("images");
                cloudClient.GetAsync("images");
                Console.WriteLine("Calling 'statistics'");
                localClient.GetAsync("statistics");
                cloudClient.GetAsync("statistics");

                Thread.Sleep(10000);
            }
        }
    }
}
