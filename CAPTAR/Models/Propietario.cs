using System.ComponentModel.DataAnnotations;

namespace CAPTAR.Models
{
    public class Propietario
    {
        public int Id { get; set; }
        [Required] public string IdDocumento { get; set; } = string.Empty;

        [Required] public required string Nombre { get; set; }
        public string? Direccion { get; set; }
        [Required] 
        public required string? Correo { get; set; }
        [Required]
        public required string Telefono { get; set; }
        public ICollection<Propiedad>? Propiedads { get; set; }
    }
}
