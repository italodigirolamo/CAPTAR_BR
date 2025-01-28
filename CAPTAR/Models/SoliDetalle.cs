using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPTAR.Models
{
    public class SoliDetalle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SoliDetalleId { get; set; }
        //public int id { get; set; }
        [Required]
        public required string Nombre{ get; set; }
        [Required]  
        public required string Direccion { get; set; }
        [Required]
        public required string Zona { get; set; }
        public string? Tipo { get; set; }
        public int PropietarioId { get; set; }
        public Propietario? propietario{ get; set; }
        //[Required]
        //public int Cantidad { get; set; }
        [Required]
        public int SolicitudId { get; set; }
        public Solicitud Solicitud { get; set; } = null!;

        //[Required]
        //public int PropiedadId { get; set; }
        //public Propiedad Propiedad { get; set; } = null!;
    }
}
