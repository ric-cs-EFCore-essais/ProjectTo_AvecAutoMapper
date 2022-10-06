using System;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

using Transverse.Common.DebugTools;

using Domaine.MyEntities;

using Infra.DataAccess;
using Infra.AutoMapper;


namespace ConsolePrj
{
    static class Program
    {
        private static MyApplicationDbContext myDbContext;

        private static IServiceProvider servicesProvider;

        static void Main()
        {
            PlaceDataIfBaseVierge();

            //------------------

            ConfigInjections();

            //------------------

            servicesProvider.GetService<MesTests>(); //Instancie la classe MesTests.

        }

        private static void ConfigInjections()
        {
            IServiceCollection servicesCollection = new ServiceCollection();

            var myAutoMapperFactory = new MyAutoMapperFactory();

            servicesCollection
                .AddSingleton((IServiceProvider poServiceProvider) =>
                {
                    return myAutoMapperFactory.GetMapper();
                })
                .AddSingleton<MesTests>() //Equivaut à AddSingleton<MesTests, MesTests>
            ;

            servicesProvider = servicesCollection.BuildServiceProvider();
        }

        //------------------------------------------

        private static void PlaceDataIfBaseVierge() //Juste pour alimenter une première fois la base, si pas déjà fait.
        {
            var myDbContextFactory = new MyApplicationDbContextFactory(appelManuel: true);
            myDbContext = myDbContextFactory.GetDbContext();

            if (estBaseVierge())
            {

                Console.WriteLine("\n\n\nRemplissage initial des tables ...\n\n");

                var personnes = new[]
                {
                    new Personne { Nom = "AAA", Prenom = "aaa" },
                    new Personne { Nom = "BBB", Prenom = "bbb" },
                    new Personne { Nom = "CCC", Prenom = "ccc" },
                    new Personne { Nom = "DDD", Prenom = "ddd" },
                    new Personne { Nom = "EEE", Prenom = "eee" },
                    new Personne { Nom = "FFF", Prenom = "fff" },
                };
                myDbContext.Personnes.AddRange(personnes);
                myDbContext.SaveChanges();


                myDbContext.Coupons.AddRange(new[]
                {
                    new Coupon() { Code = "A10", PersonneId = 1 }, //Rappel : la personne sera sous forme de PersonneId en base
                    new Coupon() { Code = "A100", PersonneId = 1 },

                    new Coupon() { Code = "B20", PersonneId = 2 },
                    new Coupon() { Code = "B200" }, //Attribué à personne exprès pour voir.

                    new Coupon() { Code = "C30", PersonneId = 3 },
                    new Coupon() { Code = "C300", PersonneId = 3 },

                    new Coupon() { Code = "D40", PersonneId = 4 },
                    new Coupon() { Code = "D400", PersonneId = 4 },

                    new Coupon() { Code = "E50", PersonneId = 5 },
                    new Coupon() { Code = "E500", PersonneId = 5 },

                    new Coupon() { Code = "F60", PersonneId = 6 },
                    new Coupon() { Code = "F600", PersonneId = 6 },
                });
                myDbContext.SaveChanges();


                Console.WriteLine("\nInit. des data de la base réalisée.\n");

            }
        }

        private static bool estBaseVierge()
        {
            return !myDbContext.Personnes.Any();
        }

    }
}
