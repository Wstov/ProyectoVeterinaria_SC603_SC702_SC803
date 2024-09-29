using Abstracciones.DA;
using Abstracciones.Modelos;
using Microsoft.Data.SqlClient;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;

        private SqlConnection _sqlConnection;

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> Agregar(Producto producto)
        {
            string sql = @"addProducto";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.Producto>(sql, new
            {
                Nombre = producto.Nombre,
                Categoria = producto.Categoria,
                Presentacion = producto.Presentacion,
                Descripcion = producto.Descripcion,
                Cantidad = producto.Cantidad,
                Precio = producto.Precio,
                Imagen = producto.Imagen
            });
            return producto.ProductoID;
        }


        public async Task<Guid> Editar(Producto producto)
        {
            string sql = @"editProducto";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.Producto>(sql, new
            {
                ProductoID = producto.ProductoID,
                Nombre = producto.Nombre,
                Categoria = producto.Categoria,
                Presentacion = producto.Presentacion,
                Descripcion = producto.Descripcion,
                Cantidad = producto.Cantidad,
                Precio = producto.Precio,
                Imagen = producto.Imagen,
            });
            return producto.ProductoID;
        }

        public async Task<Guid> Eliminar(Guid ProductoID)
        {
            string sql = @"deleteProducto";
            var consulta = await _sqlConnection.QueryAsync<Producto>(sql, new { ProductoID = ProductoID });

            return ProductoID;
        }

        public async Task<Producto> Obtener(Guid ProductoID)
        {
            string sql = @"getOneProducto";

            var consulta = await _sqlConnection.QueryAsync<Producto>(sql, new { ProductoID = ProductoID });

            if (!consulta.Any())
                return null;

            return consulta.First();
        }

        public async Task<IEnumerable<Producto>> ObtenerTodos()
        {
            string sql = @"getAllProductos";

            var consulta = await _sqlConnection.QueryAsync<Producto>(sql);

            if (!consulta.Any())
                return null;

            return consulta;
        }
    }
}
