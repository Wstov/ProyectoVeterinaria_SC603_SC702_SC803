using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase, IProductoAPI
    {
        private IProductoBW _productosBW;

        public ProductoController(IProductoBW productosBW)
        {
            _productosBW = productosBW;
        }
        [HttpPost]
        public async Task<IActionResult> Agregar(Producto producto)
        {
            return Ok(await _productosBW.Agregar(producto));
        }
        [HttpPut]
        public async Task<IActionResult> Editar(Producto producto)
        {
            return Ok(await _productosBW.Editar(producto));
        }
        [HttpDelete]
        public async Task<IActionResult> Eliminar(Guid ProductoID)
        {
            return Ok(await _productosBW.Eliminar(ProductoID));
        }
        [HttpGet]
        public async Task<IActionResult> Obtener(Guid ProductoID)
        {
            return Ok(await _productosBW.Obtener(ProductoID));
        }
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            return Ok(await _productosBW.ObtenerTodos());
        }
    }
}
