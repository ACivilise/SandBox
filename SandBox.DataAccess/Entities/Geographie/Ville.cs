using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandBox.DataAccess.Entities.Geographie
{
    public class Ville
    {
        /// <summary>
        /// Obtient ou défini l'identifiant numérique unique de la ville
        /// </summary>
        [Key]
        public int Id { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// Obtient ou défini le nom de la ville
        /// </summary>
        [Required, MaxLength(Constantes.TexteMoyen)]
        public string Libelle { get; set; }

        [MaxLength(10)]
        public string CodeINSEE { get; set; }

        /// <summary>
        /// Obtient ou défini le code postal
        /// </summary>
        [Required]
        public int IdCodePostal { get; set; }
        [ForeignKey(nameof(IdCodePostal))]
        public CodePostal CodePostal { get; set; }

        /// <summary>
        /// Obtient ou défini le pays
        /// </summary>
        [Required]
        public int IdDepartement { get; set; }
        [ForeignKey(nameof(IdDepartement))]
        public Departement Departement { get; set; }

        /// <summary>
        /// Obtient ou défini le pays
        /// </summary>
        [Required]
        public int IdPays { get; set; }
        [ForeignKey(nameof(IdPays))]
        public Pays Pays { get; set; }
    }
}
