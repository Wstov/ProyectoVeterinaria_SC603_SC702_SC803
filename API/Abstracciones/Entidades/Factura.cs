using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class Factura
    {
        public Guid FacturaID { get; set; }
        public Guid IdPersona { get; set; }
        public Guid IdCarrito { get; set; }
        public DateTime Fecha { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string? Comentarios { get; set; }
    }
}
