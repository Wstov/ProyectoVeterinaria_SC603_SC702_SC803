using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase, ICarritoAPI
    {
        private readonly ICarritoBW _carritoBW;

        public CarritoController(ICarritoBW carritoBW)
        {
            _carritoBW = carritoBW;
        }

        [HttpGet("activo/{personaId}")]
        public async Task<IActionResult> ObtenerCarritoActivo(Guid personaId)
        {
            var carrito = await _carritoBW.ObtenerCarritoActivoPorPersona(personaId);
            if (carrito == null)
            {
                return Ok(new { estado = false, mensaje = "No se encontró un carrito activo para este usuario." });
            }
            return Ok(carrito);
        }


        [HttpGet("{personaId}/detalles")]
        public async Task<IActionResult> ObtenerDetallesCarrito(Guid personaId)
        {
            var detalles = await _carritoBW.ObtenerDetallesPorCarrito(personaId);
            if (detalles == null || !detalles.Any())
                return NotFound("No se encontraron detalles en el carrito.");
            return Ok(detalles);
        }

        [HttpPost("crear/{personaId}")]
        public async Task<IActionResult> CrearCarrito(Guid personaId)
        {
            var carritoId = await _carritoBW.CrearCarrito(personaId);
            return Ok(new { CarritoID = carritoId });
        }

        [HttpPut("{carritoId}/finalizar")]
        public async Task<IActionResult> FinalizarCarrito(Guid carritoId)
        {
            await _carritoBW.FinalizarCarrito(carritoId);  
            return Ok(new { Message = "Carrito finalizado exitosamente." });
        }
        [HttpPost("{carritoId}/agregar")]
        public async Task<IActionResult> AgregarProductoAlCarrito([FromBody] DetallesCarrito detalle, Guid carritoId, Guid personaId)
        {
            if (detalle == null)
            {
                return BadRequest("Detalles del producto son necesarios.");
            }

            var productoAgregado = await _carritoBW.AgregarProductoAlCarrito(carritoId, detalle, personaId);
            return Ok(new { ProductoID = productoAgregado });
        }

        [HttpPut("{carritoId}/producto/{productoId}/actualizar")]
        public async Task<IActionResult> ActualizarCantidadProducto(Guid carritoId, Guid productoId, [FromBody] int nuevaCantidad)
        {
            await _carritoBW.ActualizarCantidadProducto(carritoId, productoId, nuevaCantidad);
            return Ok(new { Message = "Cantidad actualizada correctamente." });
        }

        [HttpDelete("{carritoId}/producto/{productoId}")]
        public async Task<IActionResult> EliminarProductoDelCarrito(Guid carritoId, Guid productoId)
        {
            await _carritoBW.EliminarProductoDelCarrito(carritoId, productoId);
            return Ok(new { Message = "Producto eliminado correctamente." });
        }
    }
}
