using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CAPTAR.Models
{
    public class Propiedad
    {
        public int id { get; set; }
        [Required]
        public required string Nombre{ get; set; }
        [Required]  
        public required string Direccion { get; set; }

        public required string Numero { get; set; }
        public required int Banos { get; set; }
        public required int Dormitorios { get; set; }
        public required bool Estacionamiento { get; set; } = false;
        [Precision(20,4)]
        public required decimal Valor { get; set; }
        public string? Tipo { get; set; }
        public string?  Imagen { get; set; }
        public  int? ZonaId { get; set; }
        public int? PropietarioId { get; set; }
        public Propietario? Propietario{ get; set; }
        public Zona? Zona{ get; set; }
    }


}
