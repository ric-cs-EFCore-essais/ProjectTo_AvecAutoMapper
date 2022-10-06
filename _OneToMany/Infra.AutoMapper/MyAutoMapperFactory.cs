using System.Collections.Generic;

using AutoMapper;

using Infra.AutoMapper.MyProfiles;


namespace Infra.AutoMapper
{
    public class MyAutoMapperFactory
    {
        private IMapper mapper;

        public IMapper GetMapper()
        {
            if (this.mapper is null)
            {
                MapperConfiguration mapperConfiguration = GetMapperConfiguration();


                this.mapper = mapperConfiguration.CreateMapper();
            }
            return this.mapper;
        }

        private MapperConfiguration GetMapperConfiguration()
        {
            var retour = new MapperConfiguration(
                (IMapperConfigurationExpression config) =>
                {
                    config.AddProfiles(GetProfiles())

                    // OU BIEN :
                    //  config.AddProfile<PersonneProfile>().AddProfile<CouponProfile>()

                    ;
                }
            );
            return retour;
        }

        private IList<Profile> GetProfiles()
        {
            return new List<Profile>()
            {
                new PersonneProfile(),
                new CouponProfile()
            };
        }
    }
}
