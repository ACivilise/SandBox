using AutoMapper;
using SandBox.DataAccess.Entities.Geographie;
using SandBox.DataAccess.Entities.Weather;
using SandBox.DTOs.DTOs.Geographie;
using SandBox.DTOs.DTOs.Weather;

namespace SandBox.DataAccess.Mapper
{
    public class DTOsProfile : Profile
    {

        public DTOsProfile()
        {
            CreateMap<Country, CountryDTO>()
                .ReverseMap();
            CreateMap<Region, RegionDTO>()
                .ReverseMap();
            CreateMap<Departement, DepartementDTO>()
                .ReverseMap();
            CreateMap<ZipCode, ZipCodeDTO>()
                .ReverseMap();
            CreateMap<City, CityDTO>()
                 .ReverseMap();
            CreateMap<Temperatures, TemperaturesDTO>()
                 .ReverseMap();

        }
    }
}
