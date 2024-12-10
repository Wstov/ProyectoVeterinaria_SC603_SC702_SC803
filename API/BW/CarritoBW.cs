using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW
{
    public class CarritoBW : ICarritoBW
    {
        private readonly ICarritoDA _carritoDA;

        public CarritoBW(ICarritoDA carritoDA)
        {
            _carritoDA = carritoDA;
        }
        public async Task<IEnumerable<DetallesCarrito>> ObtenerCarritosFinalizados(Guid carritoId)
        {
            return await _carritoDA.ObtenerCarritosFinalizados(carritoId);
        }
        public async Task<Carrito> ObtenerCarritoActivoPorPersona(Guid personaId)
        {
            return await _carritoDA.ObtenerCarritoActivoPorPersona(personaId);
        }

        public async Task<Guid> CrearCarrito(Guid personaID)
        {
            return await _carritoDA.CrearCarrito(personaID);
        }

        public async Task<Guid> FinalizarCarrito(Guid carritoID)
        {
            return await _carritoDA.FinalizarCarrito(carritoID);
        }

        public async Task<IEnumerable<DetallesCarrito>> ObtenerDetallesPorCarrito(Guid personaId)
        {
            return await _carritoDA.ObtenerDetallesPorCarrito(personaId);
        }

        public async Task<Guid> AgregarProductoAlCarrito(Guid carritoID, DetallesCarrito detalle, Guid personaId)
        {
            return await _carritoDA.AgregarProductoAlCarrito(carritoID, detalle, personaId);
        }

        public async Task<Guid> ActualizarCantidadProducto(Guid carritoID, Guid productoID, int nuevaCantidad)
        {
            return await _carritoDA.ActualizarCantidadProducto(carritoID, productoID, nuevaCantidad);
        }

        public async Task<Guid> EliminarProductoDelCarrito(Guid carritoID, Guid productoID)
        {
            return await _carritoDA.EliminarProductoDelCarrito(carritoID, productoID);
        }
    }
}
