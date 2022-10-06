using System;
using Microsoft.EntityFrameworkCore;


using Domaine.MyEntities;
using Infra.DataAccess.ModelBuilderConfigs;

namespace Infra.DataAccess
{
    public class MyApplicationDbContext : DbContext
    {
        public MyApplicationDbContext(DbContextOptions<MyApplicationDbContext> options) : base(options)
        {
            Console.WriteLine($"\n\n - Instanciation de MyApplicationDbContext -\n\n");
        }

        //--------------------------------------
        public DbSet<Personne> Personnes { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        

        //---------- Utilisation de la Fluent API, permet de paramétrer sans passer par des annotations, et donc sans toucher aux classes du Domaine ------------
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration( new PersonneConfig() );
            modelBuilder.ApplyConfiguration(new CouponConfig());

        }

    }
}
