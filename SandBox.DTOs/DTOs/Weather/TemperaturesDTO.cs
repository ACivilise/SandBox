using SandBox.DTOs.DTOs.Geographie;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.DTOs.DTOs.Weather
{
    public class TemperaturesDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TemperatureMin { get; set; }
        public decimal TemperatureMoy { get; set; }
        public decimal TemperatureMax { get; set; }

        public int? IdCity { get; set; }
        public CityDTO City { get; set; }

        public int? IdZipCode { get; set; }
        public ZipCodeDTO ZipCode { get; set; }

        public int? IdRegion { get; set; }
        public RegionDTO Region { get; set; }

        public int IdCountry { get; set; }
        public CountryDTO Country { get; set; }
    }
}
