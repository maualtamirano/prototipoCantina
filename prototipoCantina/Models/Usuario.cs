using System;
using System.Collections.Generic;

namespace prototipoCantina.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Creditos = new HashSet<Credito>();
            Pagos = new HashSet<Pago>();
            Reservas = new HashSet<Reserva>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Puesto { get; set; }

        public virtual ICollection<Credito> Creditos { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
