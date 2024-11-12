﻿using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface ICarritoDA
    {
        Task<Carrito> ObtenerCarritoActivoPorPersona(Guid PersonaID); 
        Task<Guid> CrearCarrito(Guid PersonaID);
        Task<Guid> FinalizarCarrito(Guid CarritoID);  
        Task<IEnumerable<DetallesCarrito>> ObtenerDetallesPorCarrito(Guid personaId); 
        Task<Guid> AgregarProductoAlCarrito(Guid CarritoID, DetallesCarrito detalle, Guid personaId);  
        Task<Guid> ActualizarCantidadProducto(Guid CarritoID, Guid ProductoID, int nuevaCantidad);  
        Task<Guid> EliminarProductoDelCarrito(Guid CarritoID, Guid ProductoID); 
    }

}