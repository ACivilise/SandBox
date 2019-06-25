using System.Collections.Generic;

namespace SandBox.DTOs.DTOs.Geographie
{
    public class RegionDTO
    {
        public int Id { get; set; }
        
        public string Libelle { get; set; }
        
        public string Code { get; set; }

        /// <summary>
        /// Obtient ou défini le pays
        /// </summary>
        public int IdCountry { get; set; }
        public CountryDTO Country { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des départements associés à cette region
        /// </summary>
        public virtual List<DepartementDTO> Departements { get; } = new List<DepartementDTO>();
    }
}
