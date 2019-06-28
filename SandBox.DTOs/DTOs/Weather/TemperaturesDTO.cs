using Microsoft.ML.Data;
using SandBox.DTOs.DTOs.Geographie;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.DTOs.DTOs.Weather
{
    public class TemperaturesDTO
    {
        [NoColumn]
        public int Id { get; set; }
        [NoColumn]
        public DateTime Date { get; set; }

        [NoColumn]
        public int Year { get; set; }

        [LoadColumn(0)]
        public int Month { get; set; }

        [LoadColumn(1)]
        public int Day { get; set; }

        [NoColumn]
        public int Hour { get; set; }

        [NoColumn]
        public float TemperatureMin { get; set; }

        [LoadColumn(2)]
        public float TemperatureMoy { get; set; }

        [NoColumn]
        public float TemperatureMax { get; set; }

        [LoadColumn(3)]
        public int IdRegion { get; set; }
        [NoColumn]
        public RegionDTO Region { get; set; }

        [NoColumn]
        public int? IdCity { get; set; }
        [NoColumn]
        public CityDTO City { get; set; }
        [NoColumn]
        public int? IdZipCode { get; set; }
        [NoColumn]
        public ZipCodeDTO ZipCode { get; set; }

        [NoColumn]
        public int IdCountry { get; set; }
        [NoColumn]
        public CountryDTO Country { get; set; }
    }
}
