using System.Collections.Generic;
using System.Data.Entity;
using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using MyProducts.Model;
using MyProducts.Web.TestData;
using System;
using System.Linq;

namespace MyProducts.TestData
{
    class Program
    {
        private static Dictionary<int, Category> categoryStore = new Dictionary<int, Category>();

        static void Main(string[] args)
        {
            //CreateSampleData();
            for (int i = 0; i < 100; i++)
            {
                using (var nw = new northwindEntities())
                {
                    var products = from p in nw.Products.Include("Categories")
                                   select p;
                    var x = products.ToList();

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
                                    ImageUrl = String.Format("{0}.jpg", imageNumber)
                                };

                            Category cat;
                            var catId = product.Categories.CategoryID;

                            if (categoryStore.ContainsKey(catId))
                            {
                                cat = categoryStore[catId];
                                ngmd.Entry(cat).State = EntityState.Unchanged;
                            }
                            else
                            {
                                var guid = Guid.NewGuid();
                                cat = new Category
                                {
                                    Name = product.Categories.CategoryName,
                                    Description = product.Categories.Description,
                                    Id = guid
                                };
                                categoryStore.Add(catId, cat);
                                ngmd.Categories.Add(cat);

                                ngmd.SaveChanges();
                            }

                            ngArticle.Category = cat;
                            
                            ngmd.Articles.Add(ngArticle);

                            ngmd.SaveChanges();
                        }
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
