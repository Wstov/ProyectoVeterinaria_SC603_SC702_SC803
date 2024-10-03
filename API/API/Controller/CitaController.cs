using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitaController : ControllerBase, ICitaAPI
    {
        private ICitaBW _citaBW;

        public CitaController(ICitaBW citaBW)
        {
            _citaBW = citaBW;
        }
        [HttpPost]
        public async Task<IActionResult> Agregar(Cita cita)
        {
            return Ok(await _citaBW.Agregar(cita));
        }
        [HttpPut]

        public async Task<IActionResult> Editar(Cita cita)
        {
            return Ok(await _citaBW.Editar(cita));
        }
        [HttpDelete]
        public async Task<IActionResult> Eliminar(Guid CitaID)
        {
            return Ok(await _citaBW.Eliminar(CitaID));
        }
        [HttpGet]
        public async Task<IActionResult> Obtener(Guid PersonaID)
        {
            return Ok(await _citaBW.Obtener(PersonaID));
        }
        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            return Ok(await _citaBW.ObtenerTodos());
        }
    }
}
