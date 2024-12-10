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
    public class FacturaBW : IFacturaBW
    {
        private readonly IFacturaDA _facturaDA;

        public FacturaBW(IFacturaDA facturaDA)
        {
            _facturaDA = facturaDA;
        }

        public async Task<Guid> Agregar(Factura factura)
        {
            return await _facturaDA.Agregar(factura);
        }

        public async Task<IEnumerable<Factura>> ObtenerTodos()
        {
            return await _facturaDA.ObtenerTodos();
        }

        public async Task<Factura> ObtenerOne(Guid facturaID)
        {
            return await _facturaDA.ObtenerOne(facturaID);
        }

        public async Task<IEnumerable<Factura>> ObtenerPorCliente(Guid personaID)
        {
            return await _facturaDA.ObtenerPorCliente(personaID);
        }
    }
}
