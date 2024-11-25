using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IEmpleadoDA
    {
        public Task<EmpleadoGet> ObtenerOne(Guid EmpleadoID);
        public Task<IEnumerable<EmpleadoGet>> ObtenerTodos();
        public Task<Guid> Agregar(Empleado empleado);
        public Task<Guid> Editar(Empleado empleado);
        public Task<Guid> Eliminar(Guid EmpleadoID);
    }
}
