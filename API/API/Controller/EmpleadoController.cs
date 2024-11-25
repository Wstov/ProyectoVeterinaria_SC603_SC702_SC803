using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase, IEmpleadoAPI
    {
        private readonly IEmpleadoBW _empleadoBW;

        public EmpleadoController(IEmpleadoBW empleadoBW)
        {
            _empleadoBW = empleadoBW;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Empleado empleado)
        {
            var result = await _empleadoBW.Agregar(empleado);
            return Ok(result); 
        }

        [HttpPut]
        public async Task<IActionResult> Editar(Empleado empleado)
        {
            var result = await _empleadoBW.Editar(empleado);
            return Ok(result); 
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(Guid empleadoID)
        {
            var result = await _empleadoBW.Eliminar(empleadoID);
            return Ok(result); 
        }

        [Route("one")]
        [HttpGet]
        public async Task<IActionResult> ObtenerOne(Guid empleadoID)
        {
            var empleado = await _empleadoBW.ObtenerOne(empleadoID);
            if (empleado != null)
            {
                return Ok(empleado); 
            }
            return NotFound(); 
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var empleados = await _empleadoBW.ObtenerTodos();
            return Ok(empleados);
        }
    }
}
