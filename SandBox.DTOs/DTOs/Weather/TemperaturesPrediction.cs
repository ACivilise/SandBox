using Microsoft.ML.Data;
using SandBox.DTOs.DTOs.Geographie;
using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.DTOs.DTOs.Weather
{
    public class TemperaturesPrediction
    {
        [ColumnName("TemperatureMinPrediction")]
        public decimal TemperatureMinPrediction { get; set; }

        [ColumnName("TemperatureMinScore")]
        public float[] DistancesTemperatureMinPrediction;


        [ColumnName("TemperatureMoyPrediction")]
        public decimal TemperatureMoyPrediction { get; set; }

        [ColumnName("TemperatureMoyScore")]
        public float[] DistancesTemperatureMoyPrediction;


        [ColumnName("TemperatureMaxPrediction")]
        public decimal TemperatureMaxPrediction { get; set; }

        [ColumnName("TemperatureMaxScore")]
        public float[] DistancesTemperatureMaxPrediction;
        
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
