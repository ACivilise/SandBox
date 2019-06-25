using SandBox.DataAccess.Entities.Geographie;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandBox.DataAccess.Entities.Weather
{
    public class Temperatures
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal TemperatureMin { get; set; }
        public decimal TemperatureMoy { get; set; }
        public decimal TemperatureMax { get; set; }

        public int? IdCity { get; set; }
        [ForeignKey(nameof(IdCity))]
        public City City { get; set; }

        public int? IdZipCode { get; set; }
        [ForeignKey(nameof(IdZipCode))]
        public ZipCode ZipCode { get; set; }

        public int? IdRegion { get; set; }
        [ForeignKey(nameof(IdRegion))]
        public Region Region { get; set; }

        [Required]
        public int IdCountry { get; set; }
        [ForeignKey(nameof(IdCountry))]
        public Country Country { get; set; }
    }
}
