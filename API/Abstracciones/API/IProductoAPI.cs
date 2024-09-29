using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IProductoAPI
    {
        [HttpGet]
        public Task<IActionResult> Obtener(Guid ProductoID);
        [HttpGet]
        public Task<IActionResult> Obtener();
        [HttpPost]
        public Task<IActionResult> Agregar(Producto producto);
        [HttpPut]
        public Task<IActionResult> Editar(Producto producto);
        [HttpDelete]
        public Task<IActionResult> Eliminar(Guid ProductoID);
    }
}
