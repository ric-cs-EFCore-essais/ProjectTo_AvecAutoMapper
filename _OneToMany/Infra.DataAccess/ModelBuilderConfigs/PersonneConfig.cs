using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domaine.MyEntities;


namespace Infra.DataAccess.ModelBuilderConfigs
{
    public class PersonneConfig : IEntityTypeConfiguration<Personne>
    {
        public void Configure(EntityTypeBuilder<Personne> entityModelBuilder)
        {
            entityModelBuilder.Property(personne => personne.Nom).IsRequired();

            entityModelBuilder.Property(personne => personne.Prenom).IsRequired();
        }
    }
}
