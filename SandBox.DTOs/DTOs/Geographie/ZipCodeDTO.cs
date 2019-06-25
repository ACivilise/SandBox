using System.Collections.Generic;

namespace SandBox.DTOs.DTOs.Geographie
{
    public class ZipCodeDTO
    { 
        /// <summary>
        /// Obtient ou défini l'identifiant numérique unique du code postal
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou défini la valeur du code postal
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des villes associées à ce code postal
        /// </summary>
        public virtual List<CityDTO> Cities { get; } = new List<CityDTO>();
    }
}
