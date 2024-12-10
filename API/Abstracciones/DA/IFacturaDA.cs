using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IFacturaDA
    {
        public Task<IEnumerable<Factura>> ObtenerTodos();
        public Task<Factura> ObtenerOne(Guid FacturaID);
        public Task<IEnumerable<Factura>> ObtenerPorCliente(Guid IdPersona);
        public Task<Guid> Agregar(Factura factura);
    }

}
