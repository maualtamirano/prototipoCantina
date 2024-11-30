using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prototipoCantina.Models
{
    public partial class Credito
    {
        public int IdCredito { get; set; }
        public int? IdUsuario { get; set; }
        [Display(Name = "Credito Disponible")]
        public decimal? CreditoDiario { get; set; }

        [Display(Name = "Credito Consumido")]
        public decimal? CreditoConsumido { get; set; }
        public DateTime? Fecha { get; set; }

        [Display(Name = "Usuario")]
        public virtual Usuario? IdUsuarioNavigation { get; set; }

        public decimal CreditoDisponible => (CreditoDiario ?? 0) - (CreditoConsumido ?? 0);


    }
}
