using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandBox.DataAccess.Entities.Geographie
{
    [Table("Pays")]
    public class Pays
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }

        [MaxLength(2)]
        public string ISO2 { get; set; }

        [MaxLength(3)]
        public string ISO3 { get; set; }

        [MaxLength(100) , Required]
        public string Libelle { get; set; }

        [MaxLength(100)]
        public string LibelleEN { get; set; }
        
        /// <summary>
        /// Obtient ou défini la liste des départements associés à ce pays
        /// </summary>
        public virtual List<Region> Regions { get; } = new List<Region>();

        /// <summary>
        /// Obtient ou défini la liste des Code Postaux associés à ce pays
        /// </summary>
        public virtual List<CodePostal> CodePostaux { get; } = new List<CodePostal>();

        /// <summary>
        /// Obtient ou défini la liste des Villes associés à ce pays
        /// </summary>
        public virtual List<Ville> Villes { get; } = new List<Ville>();
    }
}
