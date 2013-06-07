using FizzWare.NBuilder;
using MasterDetail.DataAccess;
using System;
using System.Linq;

namespace MasterDetail.TestData
{
    class Program
    {
        static void Main(string[] args)
        {
            var artikel = Builder<Article>.CreateListOfSize(9723)
                               .Build();

            using (var db = new ProductsEntities())
            {
                int i = 1;
                artikel.ToList().ForEach(
                    a =>
                        {
                            int imageNumber = i%6;
                            a.ImageUrl = String.Format("../images/{0}.jpg", imageNumber);
                            db.Articles.Add(a);
                            i++;
                        });

                db.SaveChanges();
            }
        }
    }
}
