using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Factura
    {
        [Required]
        public Guid FacturaID { get; set; }

        [Required]
        public Guid IdPersona { get; set; }

        [Required]
        public Guid IdCarrito { get; set; }

        [Required(ErrorMessage = "La fecha de la compra es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El método de pago debe tener entre 3 y 50 caracteres.")]
        public string MetodoPago { get; set; } = string.Empty;

        [Required(ErrorMessage = "El total es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0.")]
        public decimal Total { get; set; }

        [StringLength(250, ErrorMessage = "Los comentarios no pueden superar los 250 caracteres.")]
        public string? Comentarios { get; set; }
    }
}
