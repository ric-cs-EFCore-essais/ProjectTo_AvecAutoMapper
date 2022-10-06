using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Domaine.MyEntities;


namespace Infra.DataAccess.ModelBuilderConfigs
{
    public class CouponConfig : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> entityModelBuilder)
        {
            entityModelBuilder.Property(coupon => coupon.Code).IsRequired();

            entityModelBuilder.HasIndex(coupon => new { coupon.Code }).IsUnique();
        }
    }
}
