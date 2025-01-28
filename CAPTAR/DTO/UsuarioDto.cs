using System.ComponentModel.DataAnnotations;

namespace CAPTAR.DTO
{
    public class UsuarioDto
    {
        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(100)]
        public required string NombreCompleto { get; set; }
        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(100)]
        public required string Email { get; set; }
        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(100)]
        public required string Password { get; set; }
        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(100)]

        public required string ConfirmPass { get; set; }
        [Required]
        public required string Rol { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
