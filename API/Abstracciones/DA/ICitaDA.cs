using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface ICitaDA
    {
        public Task<IEnumerable<Guid>> Obtener(Guid PersonaID);
        public Task<IEnumerable<Cita>> ObtenerTodos();
        public Task<Guid> Agregar(Cita cita);
        public Task<Guid> Editar(Cita cita);
        public Task<Guid> Eliminar(Guid CitaID);
    }
}
