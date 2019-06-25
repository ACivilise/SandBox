namespace SandBox.DTOs.DTOs.Geographie
{
    public class CityDTO
    {
        /// <summary>
        /// Obtient ou défini l'identifiant numérique unique de la ville
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou défini le nom de la ville
        /// </summary>
        public string Libelle { get; set; }
        
        public string CodeINSEE { get; set; }

        /// <summary>
        /// Obtient ou défini l'identifiant du code postal
        /// </summary>
        public int IdZipCode { get; set; }
        /// <summary>
        /// Obtient ou défini le code postal
        /// </summary>
        public ZipCodeDTO ZipCode { get; set; }

        /// <summary>
        /// Obtient ou défini l'identifiant du pays
        /// </summary>
        public int IdDepartement { get; set; }
        /// <summary>
        /// Obtient ou défini le pays
        /// </summary>
        public DepartementDTO Departement { get; set; }

    }
}
