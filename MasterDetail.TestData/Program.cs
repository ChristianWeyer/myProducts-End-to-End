using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using FizzWare.NBuilder.PropertyNaming;
using MasterDetail.DataAccess;
using System;
using System.Linq;

namespace MasterDetail.TestData
{
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

    class Program
    {
        static void Main(string[] args)
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
}
