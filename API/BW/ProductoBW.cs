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
    public class ProductoBW : IProductoBW
    {
        private IProductoDA _productosDA;
        private IProductoImagen _productoBC;

        public ProductoBW(IProductoDA productosDA, IProductoImagen productoBC)
        {
            _productosDA = productosDA;
            _productoBC = productoBC;
        }

        public async Task<Guid> Agregar(Producto producto)
        {
            return await _productosDA.Agregar(producto);
        }

        public async Task<Guid> Editar(Producto producto)
        {
            return await _productosDA.Editar(producto);
        }

        public async Task<Guid> Eliminar(Guid ProductoID)
        {
            return await _productosDA.Eliminar(ProductoID);
        }

        public async Task<Producto> Obtener(Guid ProductoID)
        {
            return await _productosDA.Obtener(ProductoID);
        }

        public async Task<IEnumerable<Producto>> ObtenerTodos()
        {
            var productos = await _productosDA.ObtenerTodos();

            return _productoBC.ConvertirImagenes(productos);
        }
    }
}
