using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IEmpleadoAPI
    {
        [HttpGet]
        Task<IActionResult> ObtenerTodos();

        [HttpGet]
        Task<IActionResult> ObtenerOne(Guid EmpleadoID);

        [HttpPost]
        Task<IActionResult> Agregar(Empleado empleado);

        [HttpPut]
        Task<IActionResult> Editar(Empleado empleado);

        [HttpDelete]
        Task<IActionResult> Eliminar(Guid EmpleadoID);
    }
}
