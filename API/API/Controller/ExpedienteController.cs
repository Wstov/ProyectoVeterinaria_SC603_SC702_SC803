using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpedienteController : ControllerBase, IExpedienteAPI
    {
        private readonly IExpedienteBW _expedienteBW;

        public ExpedienteController(IExpedienteBW expedienteBW)
        {
            _expedienteBW = expedienteBW;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] Expediente expediente)
        {
            return Ok(await _expedienteBW.Agregar(expediente));
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Expediente expediente)
        {
            return Ok(await _expedienteBW.Editar(expediente));
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(Guid ExpedienteID)
        {
            return Ok(await _expedienteBW.Eliminar(ExpedienteID));
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPorMascota(Guid MascotaID)
        {
            return Ok(await _expedienteBW.ObtenerPorMascota(MascotaID));
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _expedienteBW.ObtenerTodos());
        }

        [Route("one")]
        [HttpGet]
        public async Task<IActionResult> ObtenerOne(Guid ExpedienteID)
        {
            return Ok(await _expedienteBW.ObtenerOne(ExpedienteID));
        }
    }
}
