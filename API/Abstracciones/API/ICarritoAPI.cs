using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface ICarritoAPI
    {
        [HttpGet("api/carrito/activo/{personaId}")]
        Task<IActionResult> ObtenerCarritoActivo(Guid personaId);
        [HttpGet("api/carrito/{carritoId}/detalles")]
        Task<IActionResult> ObtenerDetallesCarrito(Guid personaId);
        [HttpPost("api/carrito/crear/{personaId}")]
        Task<IActionResult> CrearCarrito( Guid personaId);
        [HttpPut("api/carrito/{carritoId}/finalizar")]
        Task<IActionResult> FinalizarCarrito(Guid carritoId);
        [HttpPost("api/carrito/{carritoId}/agregar")]
        Task<IActionResult> AgregarProductoAlCarrito([FromBody] DetallesCarrito detalle, Guid carritoId, Guid personaId);

        [HttpPut("api/carrito/{carritoId}/producto/{productoId}/actualizar")]
        Task<IActionResult> ActualizarCantidadProducto(Guid carritoId, Guid productoId, [FromBody] int nuevaCantidad);
        [HttpDelete("api/carrito/{carritoId}/producto/{productoId}")]
        Task<IActionResult> EliminarProductoDelCarrito(Guid carritoId, Guid productoId);
        [HttpGet("api/carrito/finalizados")]
        Task<IActionResult> ObtenerCarritosFinalizados(Guid carritoId);
    }
}
