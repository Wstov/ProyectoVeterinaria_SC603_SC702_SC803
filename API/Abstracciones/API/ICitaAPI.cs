using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface ICitaAPI
    {
        [HttpGet]
        public Task<IActionResult> Obtener(Guid PersonaID);
        [HttpGet]
        public Task<IActionResult> Obtener();
        [HttpGet]
        public Task<IActionResult> ObtenerOne(Guid CitaID);
        [HttpPost]
        public Task<IActionResult> Agregar(Cita cita);
        [HttpPut]
        public Task<IActionResult> Editar(Cita cita);
        [HttpDelete]
        public Task<IActionResult> Eliminar(Guid CitaID);
    }
}
