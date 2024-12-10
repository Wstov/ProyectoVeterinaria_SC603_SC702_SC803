using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IFacturaAPI
    {
        [HttpGet]
        Task<IActionResult> ObtenerTodos();

        [HttpGet]
        Task<IActionResult> ObtenerOne(Guid FacturaID);

        [HttpGet]
        Task<IActionResult> ObtenerPorCliente(Guid IdPersona);

        [HttpPost]
        Task<IActionResult> Agregar(Factura factura);
    }
}
