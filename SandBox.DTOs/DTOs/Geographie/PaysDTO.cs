using System.Collections.Generic;

namespace SandBox.DTOs.DTOs.Geographie
{
    public class CountryDTO
    {
        public int Id { get; set; }
        
        public string Code { get; set; }
        
        public string ISO2 { get; set; }
        
        public string ISO3 { get; set; }
        
        public string Libelle { get; set; }
        
        public string LibelleEN { get; set; }
        /// <summary>
        /// Obtient ou défini la liste des départements associés à ce pays
        /// </summary>
        public virtual List<RegionDTO> Regions { get; } = new List<RegionDTO>();

        /// <summary>
        /// Obtient ou défini la liste des Code Postaux associés à ce pays
        /// </summary>
        public virtual List<ZipCodeDTO> ZipCodes { get; } = new List<ZipCodeDTO>();

        /// <summary>
        /// Obtient ou défini la liste des Villes associés à ce pays
        /// </summary>
        public virtual List<CityDTO> Cities { get; } = new List<CityDTO>();
    }
}
