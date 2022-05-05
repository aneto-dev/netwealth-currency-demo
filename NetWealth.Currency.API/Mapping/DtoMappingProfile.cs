using AutoMapper;
using NetWealth.Data.Entities;
using NetWealth.Data.Models.Dto;

namespace NetWealth.Currency.API.Mapping
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<CountryCurrency, CountryCurrencyDto>();
            CreateMap<CountryCurrencyDto, CountryCurrency>();
        }
    }
}
