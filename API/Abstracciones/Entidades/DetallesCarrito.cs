using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class DetallesCarrito
    {
        [Required]
        public Guid DetallesCarritoID { get; set; }

        [Required]
        public Guid CarritoID { get; set; }

        [Required]
        public Guid ProductoID { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser positivo.")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public decimal MontoTotal
        {
            get { return PrecioUnitario * Cantidad; }
        }
    }
}
