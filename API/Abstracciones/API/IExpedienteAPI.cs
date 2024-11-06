using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IExpedienteAPI
    {
        [HttpGet]
        public Task<IActionResult> ObtenerPorMascota(Guid MascotaID);

        [HttpGet]
        public Task<IActionResult> ObtenerTodos();

        [HttpGet]
        public Task<IActionResult> ObtenerOne(Guid ExpedienteID);

        [HttpPost]
        public Task<IActionResult> Agregar([FromBody] Expediente expediente);

        [HttpPut]
        public Task<IActionResult> Editar([FromBody] Expediente expediente);

        [HttpDelete]
        public Task<IActionResult> Eliminar(Guid ExpedienteID);
    }
}
