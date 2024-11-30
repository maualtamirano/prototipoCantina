using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prototipoCantina.Models
{
    public partial class Menu
    {
        public Menu()
        {
            Reservas = new HashSet<Reserva>();
        }

        public int IdMenu { get; set; }
        [Display(Name = "Menu")]
        public string? NombreMenu { get; set; }
        public decimal? Precio { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
