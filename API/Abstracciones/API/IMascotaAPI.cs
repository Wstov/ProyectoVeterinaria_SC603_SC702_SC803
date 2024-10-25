using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IMascotaAPI
    {
        [HttpGet]
        Task<IActionResult> Obtener(Guid PersonaID);

        [HttpGet]
        Task<IActionResult> Obtener();

        [HttpGet]
        Task<IActionResult> ObtenerOne(Guid MascotaID);

        [HttpPost]
        Task<IActionResult> Agregar(Mascota mascota);

        [HttpPut]
        Task<IActionResult> Editar(Mascota mascota);

        [HttpDelete]
        Task<IActionResult> Eliminar(Guid MascotaID);
    }

}
