
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BW
{
    public interface IProductoBW
    {
        public Task<Producto> Obtener(Guid ProductoID);
        public Task<IEnumerable<Producto>> ObtenerTodos();
        public Task<Guid> Agregar(Producto producto);
        public Task<Guid> Editar(Producto producto);
        public Task<Guid> Eliminar(Guid ProductoID);
    }
}
