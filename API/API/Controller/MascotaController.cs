using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase, IMascotaAPI
    {
        private readonly IMascotaBW _mascotaBW;

        public MascotaController(IMascotaBW mascotaBW)
        {
            _mascotaBW = mascotaBW;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Mascota mascota)
        {
            return Ok(await _mascotaBW.Agregar(mascota));
        }

        [HttpPut]
        public async Task<IActionResult> Editar(Mascota mascota)
        {
            return Ok(await _mascotaBW.Editar(mascota));
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(Guid mascotaID)
        {
            return Ok(await _mascotaBW.Eliminar(mascotaID));
        }

        [HttpGet]
        public async Task<IActionResult> Obtener(Guid personaID)
        {
            return Ok(await _mascotaBW.Obtener(personaID));
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            return Ok(await _mascotaBW.ObtenerTodos());
        }

        [Route("one")]
        [HttpGet]
        public async Task<IActionResult> ObtenerOne(Guid mascotaID)
        {
            return Ok(await _mascotaBW.ObtenerOne(mascotaID));
        }
    }

}
