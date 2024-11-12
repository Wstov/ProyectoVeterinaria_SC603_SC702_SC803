using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class Carrito
    {
        [Required]
        public Guid CarritoID { get; set; }

        [Required]
        public Guid PersonaID { get; set; }

        [Required]
        public bool Estado { get; set; }

        public List<DetallesCarrito> Detalles { get; set; } = new List<DetallesCarrito>();
    }
}