using CAPTAR.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace CAPTAR.DTO
{
    public class SolicitudDto
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(100)]
        public required string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(100)]
        [EmailAddress() ]
        public required string Email { get; set; }


        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(100)]
        public string? Contacto { get; set; }

        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(100)]
        public required string Direccion { get; set; }

        [Required(ErrorMessage = "El campo es requerido, por favor ingrese uno válido")]
        [StringLength(69)]
        public required string Telefono { get; set; }
        [Display(Name = "N° Departmo/Casa")]
        public string? Numero { get; set; }

        [Display(Name = "Valor estimado")]
        public decimal? Valor { get; set; }
        [Display(Name = "Baños")]
        public int Banos { get; set; }
        public int Dormitorios { get; set; }
        [Required]
        public required int Estacionamiento { get; set; }
        public IFormFile? Imagen { get; set; }  
        [Display(Name = "")]
        public DateTime Fecha { get; set; } = DateTime.Now;
        // public ICollection<Zona>? Zona { get; set; }
       public string Zona { get; set; } = string.Empty;

        //public Zona zona { get; set; }
    }
}
