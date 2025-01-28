using System.ComponentModel.DataAnnotations;

namespace CAPTAR.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public required string NombreCompleto { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Rol { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
