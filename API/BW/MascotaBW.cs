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
    public class MascotaBW : IMascotaBW
    {
        private readonly IMascotaDA _mascotaDA;

        public MascotaBW(IMascotaDA mascotaDA)
        {
            _mascotaDA = mascotaDA;
        }

        public async Task<Guid> Agregar(Mascota mascota)
        {
            return await _mascotaDA.Agregar(mascota);
        }

        public async Task<Guid> Editar(Mascota mascota)
        {
            return await _mascotaDA.Editar(mascota);
        }

        public async Task<Guid> Eliminar(Guid mascotaID)
        {
            return await _mascotaDA.Eliminar(mascotaID);
        }

        public async Task<IEnumerable<Mascota>> Obtener(Guid personaID)
        {
            return await _mascotaDA.Obtener(personaID);
        }

        public async Task<Mascota> ObtenerOne(Guid mascotaID)
        {
            return await _mascotaDA.ObtenerOne(mascotaID);
        }

        public async Task<IEnumerable<Mascota>> ObtenerTodos()
        {
            return await _mascotaDA.ObtenerTodos();
        }
    }

}
