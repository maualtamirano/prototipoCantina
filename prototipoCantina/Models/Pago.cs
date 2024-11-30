using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prototipoCantina.Models
{
    public partial class Pago
    {
        public int IdPago { get; set; }
        public int? IdUsuario { get; set; }
        public decimal Monto { get; set; }
        [Display(Name = "Fecha de Pago")]
        public DateTime? FechaPago { get; set; }
        [Display(Name = "Comprador")]
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
