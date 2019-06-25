using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandBox.DataAccess.Entities.Geographie
{
    public class ZipCode
    {
        /// <summary>
        /// Obtient ou défini l'identifiant numérique unique du code postal
        /// </summary>
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// Obtient ou défini la valeur du code postal
        /// </summary>
        [MaxLength(10)]
        [Required]
        public string Libelle { get; set; }

        /// <summary>
        /// Obtient ou défini le pays
        /// </summary>
        [Required]
        public int IdPays { get; set; }
        [ForeignKey(nameof(IdPays))]
        public Country Pays { get; set; }

        /// <summary>
        /// Obtient ou défini la liste des villes associées à ce code postal
        /// </summary>
        public virtual List<City> Villes { get; } = new List<City>();
    }
}
