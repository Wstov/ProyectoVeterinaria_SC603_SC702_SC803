using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IMascotaDA
    {
        public Task<IEnumerable<Mascota>> Obtener(Guid PersonaID);
        public Task<Mascota> ObtenerOne(Guid MascotaID);

        public Task<IEnumerable<Mascota>> ObtenerTodos();
        public Task<Guid> Agregar(Mascota mascota);
        public Task<Guid> Editar(Mascota mascota);
        public Task<Guid> Eliminar(Guid MascotaID);
    }
}
