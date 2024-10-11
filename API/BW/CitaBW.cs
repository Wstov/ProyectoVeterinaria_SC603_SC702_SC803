using Abstracciones.BC;
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
    public class CitaBW : ICitaBW
    {
        private ICitaDA _citaDA;

        public CitaBW(ICitaDA citaDA)
        {
            _citaDA = citaDA;
        }
        public async Task<Guid> Agregar(Cita cita)
        {
            return await _citaDA.Agregar(cita);
        }

        public async Task<Guid> Editar(Cita cita)
        {
            return await _citaDA.Editar(cita);
        }

        public async Task<Guid> Eliminar(Guid CitaID)
        {
            return await _citaDA.Eliminar(CitaID);
        }

        public async Task<IEnumerable<Cita>> Obtener(Guid PersonaID)
        {
           return await _citaDA.Obtener(PersonaID);
        }

        public async Task<Cita> ObtenerOne(Guid CitaID)
        {
            return await _citaDA.ObtenerOne(CitaID);
        }

        public async Task<IEnumerable<Cita>> ObtenerTodos()
        {
            return await _citaDA.ObtenerTodos();
        }
    }
}
