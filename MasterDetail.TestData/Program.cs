using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using MasterDetail.DataAccess;
using System;
using System.Linq;

namespace MasterDetail.TestData
{
    class Program
    {
        static void Main(string[] args)
        {
            //CreateSampleData();
            for (int i = 0; i < 100; i++)
            {
                using (var nw = new northwindEntities())
                {
                    var products = from p in nw.Products
                                   select p;

                    using (var ngmd = new ProductsContext())
                    {
                        var rnd = new Random();

                        foreach (var product in products)
                        {
                            var imageNumber = rnd.Next(0, 6);
                            var ngArticle = new Article
                            {
                                Id = Guid.NewGuid(),
                                Name = product.ProductName,
                                Code = product.QuantityPerUnit,
                                Description =
                                    "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.",
                                ImageUrl = String.Format("images/{0}.jpg", imageNumber)
                            };

                            ngmd.Articles.Add(ngArticle);
                        }

                        ngmd.SaveChanges();
                    }
                }
            }

            
        }

        private static void CreateSampleData()
        {
            BuilderSetup.SetDefaultPropertyNamer(new MyPropertyNamer(new ReflectionUtil()));
            var artikel = Builder<Article>.CreateListOfSize(27859)
                                          .Build();

            using (var db = new ProductsContext())
            {
                int i = 1;
                artikel.ToList().ForEach(
                    a =>
                    {
                        int imageNumber = i % 6;
                        a.ImageUrl = String.Format("images/{0}.jpg", imageNumber);
                        db.Articles.Add(a);
                        i++;
                    });

                db.SaveChanges();
            }

            Console.WriteLine("Done!");
        }
    }

    class MyPropertyNamer : SequentialPropertyNamer
    {
        public MyPropertyNamer(IReflectionUtil reflectionUtil)
            : base(reflectionUtil)
        {
        }

        protected override Guid GetGuid(System.Reflection.MemberInfo memberInfo)
        {
            return Guid.NewGuid();
        }
    }
}
