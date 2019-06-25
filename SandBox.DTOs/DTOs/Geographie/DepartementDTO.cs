using System.Collections.Generic;
using System.ComponentModel;

namespace SandBox.DTOs.DTOs.Geographie
{
    public class DepartementDTO
    {
        public int Id { get; set; }

        public string Libelle { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// Obtient ou défini l'identifiant du pays
        /// </summary>
        public int IdRegion { get; set; }
        /// <summary>
        /// Obtient ou défini le pays
        /// </summary>
        public RegionDTO Region { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des villes associées à ce département
        /// </summary>
        public virtual List<CityDTO> Cities { get; } = new List<CityDTO>();
    }
}
