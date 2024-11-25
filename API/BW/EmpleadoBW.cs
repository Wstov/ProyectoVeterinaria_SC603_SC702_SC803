using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW
{
    public class EmpleadoBW : IEmpleadoBW
    {
        private readonly IEmpleadoDA _empleadoDA;

        public EmpleadoBW(IEmpleadoDA empleadoDA)
        {
            _empleadoDA = empleadoDA;
        }

        public async Task<Guid> Agregar(Empleado empleado)
        {
            return await _empleadoDA.Agregar(empleado);
        }

        public async Task<Guid> Editar(Empleado empleado)
        {
            return await _empleadoDA.Editar(empleado);
        }

        public async Task<Guid> Eliminar(Guid empleadoID)
        {
            return await _empleadoDA.Eliminar(empleadoID);
        }

        public async Task<EmpleadoGet> ObtenerOne(Guid empleadoID)
        {
            return await _empleadoDA.ObtenerOne(empleadoID);
        }

        public async Task<IEnumerable<EmpleadoGet>> ObtenerTodos()
        {
            return await _empleadoDA.ObtenerTodos();
        }
    }
}
