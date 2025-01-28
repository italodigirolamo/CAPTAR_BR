using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CAPTAR.Models
{
    public class Zona
    {
        public int id { get; set; }
        [Required]
        public required string Nombre{ get; set; }
        [Required]  
        public required string Descripcion { get; set; }
      //  public Collection<Propiedad>? Propiedads { get; set; }
        public ICollection<Propiedad>? Propiedads { get; set; }


    }
}
