using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase, IFacturaAPI
    {
        private readonly IFacturaBW _facturaBW;

        public FacturaController(IFacturaBW facturaBW)
        {
            _facturaBW = facturaBW;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(Factura factura)
        {
            return Ok(await _facturaBW.Agregar(factura));
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            return Ok(await _facturaBW.ObtenerTodos());
        }

        [Route("one")]
        [HttpGet]
        public async Task<IActionResult> ObtenerOne(Guid facturaID)
        {
            return Ok(await _facturaBW.ObtenerOne(facturaID));
        }

        [Route("cliente")]
        [HttpGet]
        public async Task<IActionResult> ObtenerPorCliente(Guid personaID)
        {
            return Ok(await _facturaBW.ObtenerPorCliente(personaID));
        }
    }
}
