using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandBox.DataAccess.Entities.Geographie
{
    public class Departement
    {
        [Key]
        public int Id { get; set; }

        public string Libelle { get; set; }

        [MaxLength(20)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Obtient ou défini la region
        /// </summary>
        [Required]
        public int IdRegion { get; set; }
        [ForeignKey(nameof(IdRegion))]
        public Region Region { get; set; }
        
        /// <summary>
        /// Obtient ou défini la liste des villes associées à ce département
        /// </summary>
        public virtual List<City> Villes { get; } = new List<City>();

    }
}
