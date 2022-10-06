using System;
using System.Linq;

using Microsoft.EntityFrameworkCore; //Pour la méthode Include() d'un DbSet, ainsi que pour la méthode AsNoTracking().

using AutoMapper;
using AutoMapper.QueryableExtensions; //Pour ProjectTo() sur un DbSet !

using Transverse.Common.DebugTools;

using Infra.DataAccess;
using Infra.Api.DTOs;


namespace ConsolePrj
{
    public class MesTests
    {
        private readonly IMapper mapper;
        private readonly Chrono chrono;

        public MesTests(IMapper mapper)
        {
            chrono = new Chrono();

            this.mapper = mapper;


            Test_PersonnesToPersonnesDTO_via_Select();
            Test_PersonnesToPersonnesDTO_via_Include__Select_et_AutoMapper_Map();


            Test_PersonnesToPersonnesDTO_via_ProjectTo_DuDbSetEnrichi();
            Test_PersonnesToPersonnesDTO_via_ProjectTo_DuMapper();

        }

        private MyApplicationDbContext GetNewDbContext() //Réinstanciation du DbContext à chaque Test, pour ne pas être pollué par les éventuelles mises en cache d'EF.
        {
            var myDbContextFactory = new MyApplicationDbContextFactory(appelManuel: true);
            MyApplicationDbContext myDbContext = myDbContextFactory.GetDbContext();
            return myDbContext;
        }




        public void Test_PersonnesToPersonnesDTO_via_ProjectTo_DuDbSetEnrichi()
        {
            Console.WriteLine("\n\n\n\n--------- Test_PersonnesToPersonnesDTO_via_ProjectTo_DuDbSetEnrichi ---------\n");
            MyApplicationDbContext myDbContext = GetNewDbContext();

            chrono.Start();
            Debug.ShowData(
                myDbContext.Personnes.ProjectTo<PersonneDTO>(mapper.ConfigurationProvider).ToList() //Conversion chaque Personne en PersonneDTO suivant PersonneProfile.
                                                                                                    // REM.: DbSet enrichi (méthode ProjectTo), grâce au : using AutoMapper.QueryableExtensions 
            );
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }

        public void Test_PersonnesToPersonnesDTO_via_ProjectTo_DuMapper()
        {
            Console.WriteLine("\n\n\n\n--------- Test_PersonnesToPersonnesDTO_via_ProjectTo_DuMapper ---------\n");
            MyApplicationDbContext myDbContext = GetNewDbContext();

            chrono.Start();
            Debug.ShowData(
                mapper.ProjectTo<PersonneDTO>(myDbContext.Personnes).ToList() //Conversion chaque Personne en PersonneDTO suivant PersonneProfile.
            );
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }




        public void Test_PersonnesToPersonnesDTO_via_Include__Select_et_AutoMapper_Map()
        {
            Console.WriteLine("\n\n\n\n--------- Test_PersonnesToPersonnesDTO_via_Include__Select_et_AutoMapper.Map ---------\n");
            MyApplicationDbContext myDbContext = GetNewDbContext();

            chrono.Start();
            Debug.ShowData(
                myDbContext.Personnes.Include(personne => personne.Coupons).Select(personne => mapper.Map<PersonneDTO>(personne)).ToList()
                //myDbContext.Personnes.Select(personne => mapper.Map<PersonneDTO>(personne)).ToList() //<<<Sans le Include(), évidemment la liste des Coupons de chaque personne, ne sera pas renseignée par EF !!
            );
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }


        public void Test_PersonnesToPersonnesDTO_via_Select()
        {
            Console.WriteLine("\n\n\n\n--------- Test_PersonnesToPersonnesDTO_via_Select  (projection personne.Coupons à la mano.) ---------\n");
            MyApplicationDbContext myDbContext = GetNewDbContext();

            chrono.Start();
            Debug.ShowData(
                myDbContext.Personnes.Select(personne => new PersonneDTO()
                { //Conversion A LA MANO., de chaque Personne en PersonneDTO.
                    Id = personne.Id,
                    NomComplet = $"{personne.Nom} - {personne.Prenom}",
                    Coupons = personne.Coupons.Select(coupon => new CouponDTO()
                    {
                        ID = coupon.Id,
                        CODE = coupon.Code
                    }).ToList()
                }
               )
            );
            chrono.StopAndShowDuration();

            Console.ReadKey();
        }

    }
}