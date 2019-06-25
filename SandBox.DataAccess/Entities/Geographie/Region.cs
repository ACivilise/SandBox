using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandBox.DataAccess.Entities.Geographie
{
    public class Region
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Libellé")]
        public string Libelle { get; set; }

        [DisplayName("Code region")]
        public string Code { get; set; }

        /// <summary>
        /// Obtient ou défini le pays
        /// </summary>
        [Required]
        public int IdPays { get; set; }
        [ForeignKey(nameof(IdPays))]
        public Pays Pays { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des départements associés à cette region
        /// </summary>
        public virtual List<Departement> Departements { get; } = new List<Departement>();
    }
}
