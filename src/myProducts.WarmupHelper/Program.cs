using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace myProducts.WarmupHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://windows8vm/ngmd/api/");
            client.SetBasicAuthentication("cw", "cw");

            while (true)
            {
                Console.WriteLine("Calling 'personalization'");
                client.GetAsync("personalization");
                Console.WriteLine("Calling 'articles'");
                client.GetAsync("articles");
                Console.WriteLine("Calling 'images'");
                client.GetAsync("images");
                Console.WriteLine("Calling 'statistics'");
                client.GetAsync("statistics");

                Thread.Sleep(10000);
            }
        }
    }
}
