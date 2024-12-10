using Abstracciones.DA;
using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class CarritoDA : ICarritoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public CarritoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }
        public async Task<IEnumerable<DetallesCarrito>> ObtenerCarritosFinalizados(Guid carritoId)
        {
            string sql = "ObtenerCarritosFinalizados";
            var result = await _sqlConnection.QueryAsync<DetallesCarrito>(
                sql, new { carritoId=carritoId},
                commandType: CommandType.StoredProcedure
            );
            return result;
        }
        public async Task<Carrito> ObtenerCarritoActivoPorPersona(Guid personaId)
        {
            string sql = @"ObtenerCarritoActivo";
            var carrito = await _sqlConnection.QueryFirstOrDefaultAsync<Carrito>(sql, new { PersonaID = personaId }, commandType: CommandType.StoredProcedure);
            Console.WriteLine($"CarritoID: {carrito?.CarritoID}, PersonaID: {carrito?.PersonaID}, Estado: {carrito?.Estado}");
            return carrito;
        }

        public async Task<Guid> CrearCarrito(Guid personaId)
        {
            string sql = @"CrearCarrito"; 
            var carritoId = await _sqlConnection.ExecuteScalarAsync<Guid>(
                sql, new { PersonaID = personaId }, commandType: CommandType.StoredProcedure);
            return carritoId;
        }
        public async Task<Guid> FinalizarCarrito(Guid carritoId)
        {
            string sql = @"CerrarCarrito";  
            await _sqlConnection.ExecuteAsync(sql, new { CarritoID = carritoId }, commandType: CommandType.StoredProcedure);
            return carritoId;
        }

        public async Task<IEnumerable<DetallesCarrito>> ObtenerDetallesPorCarrito(Guid personaId)
        {
            string sql = @"ObtenerCarritoUsuario"; 
            var detalles = await _sqlConnection.QueryAsync<DetallesCarrito>(sql, new { personaId = personaId }, commandType: CommandType.StoredProcedure);
            return detalles;
        }
        public async Task<Guid> AgregarProductoAlCarrito(Guid carritoId, DetallesCarrito detalle, Guid personaId)
        {
            string sql = @"AgregarProductoCarrito";
            await _sqlConnection.ExecuteAsync(sql, new
            {
                PersonaID = personaId,  
                ProductoID = detalle.ProductoID,
                Cantidad = detalle.Cantidad
            }, commandType: CommandType.StoredProcedure);

            return carritoId;
        }


        public async Task<Guid> ActualizarCantidadProducto(Guid carritoId, Guid productoId, int nuevaCantidad)
        {
            string sql = @"EditarCantidadProductoCarrito";  
            await _sqlConnection.ExecuteAsync(sql, new
            {
                CarritoID = carritoId,
                ProductoID = productoId,
                NuevaCantidad = nuevaCantidad
            }, commandType: CommandType.StoredProcedure);
            return carritoId;
        }

        public async Task<Guid> EliminarProductoDelCarrito(Guid carritoId, Guid productoId)
        {
            string sql = @"EliminarProductoCarrito"; 
            await _sqlConnection.ExecuteAsync(sql, new { CarritoID = carritoId, ProductoID = productoId }, commandType: CommandType.StoredProcedure);
            return carritoId;
        }

        public async Task<Guid> LimpiarCarrito(Guid carritoId)
        {
            string sql = @"LimpiarCarrito";  
            await _sqlConnection.ExecuteAsync(sql, new { CarritoID = carritoId }, commandType: CommandType.StoredProcedure);
            return carritoId;
        }
    }
}
