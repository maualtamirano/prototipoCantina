using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prototipoCantina.Models
{
    public partial class Reserva
    {
        public int IdReserva { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdMenu { get; set; } 
        [Display(Name = "Fecha de Reserva")]
        public DateTime FechaReserva { get; set; }
        [Display(Name = "Monto Total")]
        public decimal MontoTotal { get; set; }
        [Display(Name = "Menu Seleccionado")]

        public virtual Menu? IdMenuNavigation { get; set; }
        [Display(Name = "Usuario de la orden")]

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
