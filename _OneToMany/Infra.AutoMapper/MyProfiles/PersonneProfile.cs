using AutoMapper;

using Domaine.MyEntities;

using Infra.Api.DTOs;

namespace Infra.AutoMapper.MyProfiles
{
    public class PersonneProfile: Profile
    {
        public PersonneProfile()
        {
            CreateMap<Personne, PersonneDTO>()
                .ForMember(poDesti => poDesti.NomComplet, opt => opt.MapFrom(poSource => $"{poSource.Nom} - {poSource.Prenom}"));
        }
    }
}
