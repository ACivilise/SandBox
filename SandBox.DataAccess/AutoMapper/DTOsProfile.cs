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
                 .ForMember(dest => dest.Year, opts => opts.MapFrom(src => src.Date.Year))
                 .ForMember(dest => dest.Month, opts => opts.MapFrom(src => src.Date.Month))
                 .ForMember(dest => dest.Day, opts => opts.MapFrom(src => src.Date.Day))
                 .ForMember(dest => dest.Hour, opts => opts.MapFrom(src => src.Date.Hour))
                 .ForMember(dest => dest.TemperatureMax, opts => opts.MapFrom(src => (float)src.TemperatureMax))
                 .ForMember(dest => dest.TemperatureMin, opts => opts.MapFrom(src => (float)src.TemperatureMin))
                 .ForMember(dest => dest.TemperatureMoy, opts => opts.MapFrom(src => (float)src.TemperatureMoy))
                 .ForMember(dest => dest.IdRegion, opts => opts.MapFrom(src =>src.IdRegion))
                 .ReverseMap()
                 .ForMember(dest => dest.TemperatureMax, opts => opts.MapFrom(src => (decimal)src.TemperatureMax))
                 .ForMember(dest => dest.TemperatureMin, opts => opts.MapFrom(src => (decimal)src.TemperatureMin))
                 .ForMember(dest => dest.TemperatureMoy, opts => opts.MapFrom(src => (decimal)src.TemperatureMoy))
                 .ForMember(dest => dest.IdRegion, opts => opts.MapFrom(src => src.IdRegion));

        }
    }
}
