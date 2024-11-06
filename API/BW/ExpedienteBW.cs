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
    public class ExpedienteBW : IExpedienteBW
    {
        private readonly IExpedienteDA _expedienteDA;

        public ExpedienteBW(IExpedienteDA expedienteDA)
        {
            _expedienteDA = expedienteDA;
        }

        public async Task<Guid> Agregar(Expediente expediente)
        {
            return await _expedienteDA.Agregar(expediente);
        }

        public async Task<Guid> Editar(Expediente expediente)
        {
            return await _expedienteDA.Editar(expediente);
        }

        public async Task<Guid> Eliminar(Guid ExpedienteID)
        {
            return await _expedienteDA.Eliminar(ExpedienteID);
        }

        public async Task<IEnumerable<Expediente>> ObtenerPorMascota(Guid MascotaID)
        {
            return await _expedienteDA.ObtenerPorMascota(MascotaID);
        }

        public async Task<Expediente> ObtenerOne(Guid ExpedienteID)
        {
            return await _expedienteDA.ObtenerOne(ExpedienteID);
        }

        public async Task<IEnumerable<Expediente>> ObtenerTodos()
        {
            return await _expedienteDA.ObtenerTodos();
        }
    }
}
