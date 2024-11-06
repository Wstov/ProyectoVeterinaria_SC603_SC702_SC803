using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IExpedienteDA
    {
        public Task<IEnumerable<Expediente>> ObtenerPorMascota(Guid MascotaID);
        public Task<Expediente> ObtenerOne(Guid ExpedienteID);
        public Task<IEnumerable<Expediente>> ObtenerTodos();
        public Task<Guid> Agregar(Expediente expediente);
        public Task<Guid> Editar(Expediente expediente);
        public Task<Guid> Eliminar(Guid ExpedienteID);
    }
}
