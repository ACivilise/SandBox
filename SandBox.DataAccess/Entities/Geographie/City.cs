using SandBox.Constantes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SandBox.DataAccess.Entities.Geographie
{
    public class City
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
        [Required, MaxLength(FieldsSizes.TexteMoyen)]
        public string Libelle { get; set; }

        [MaxLength(10)]
        public string CodeINSEE { get; set; }

        /// <summary>
        /// Obtient ou défini le code postal
        /// </summary>
        [Required]
        public int IdZipCode { get; set; }
        [ForeignKey(nameof(IdZipCode))]
        public ZipCode ZipCode { get; set; }

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
        public int IdCountry { get; set; }
        [ForeignKey(nameof(IdCountry))]
        public Country Country { get; set; }
    }
}
