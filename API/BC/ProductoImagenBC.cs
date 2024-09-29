using Abstracciones.BC;
using Abstracciones.Modelos;


namespace BC
{
    public class ProductoImagenBC : IProductoImagen
    {
        public IEnumerable<Producto> ConvertirImagenes(IEnumerable<Producto> productos)
        {
            if (productos != null)
            {
                foreach (Producto productosItem in productos)
                {
                    if (productosItem.Imagen == null || productosItem.Imagen.Length == 0)
                    {
                        break;
                    }

                    productosItem.ImagenConvertida = Convert.ToBase64String(productosItem.Imagen);
                }
            }

            return productos;
        }
    }
}
