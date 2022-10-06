using AutoMapper;

using Domaine.MyEntities;

using Infra.Api.DTOs;

namespace Infra.AutoMapper.MyProfiles
{
    public class CouponProfile: Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDTO>();
        }
    }
}
