using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Text.Json;



namespace WEB.Pages.Productos
{
    [Authorize(Roles = "1,2,3")]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly Configuracion _configuration;

        public IList<Producto> producto { get; set; } = default!;
        public string SearchTerm { get; set; }

        public IndexModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGetAsync(string? searchTerm)
        {
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            string urlEndPoint = _configuration.ObtenerEndPoint("getAllProducto");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, urlEndPoint);
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                producto = JsonSerializer.Deserialize<List<Producto>>(resultado, options) ?? new List<Producto>();

                // Filtrar productos si se ingresó un término de búsqueda
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    SearchTerm = searchTerm.ToLower();
                    producto = producto.Where(p => p.Nombre.ToLower().Contains(SearchTerm)).ToList();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAgregarAlCarritoAsync(Guid productoId, int cantidad, Guid personaId)
        {
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            string urlAgregarCarrito = _configuration.ObtenerEndPoint("addtoCarrito");

            var detalle = new { ProductoID = productoId, Cantidad = cantidad };

            var respuesta = await cliente.PostAsJsonAsync($"{urlAgregarCarrito}/{personaId}", detalle);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("/Carrito/Index");
            }

            ModelState.AddModelError(string.Empty, "Error al agregar el producto al carrito.");
            return Page();
        }

        public async Task<IActionResult> OnPostActualizarCantidadAsync(Guid carritoId, Guid productoId, int nuevaCantidad)
        {
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            string urlActualizarCantidad = _configuration.ObtenerEndPoint("actualizarCantidadCarrito");

            var respuesta = await cliente.PutAsJsonAsync($"{urlActualizarCantidad}/{carritoId}/producto/{productoId}/actualizar", nuevaCantidad);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("/Carrito/Index");
            }

            ModelState.AddModelError(string.Empty, "Error al actualizar la cantidad del producto en el carrito.");
            return Page();
        }

        public async Task<IActionResult> OnPostEliminarProductoAsync(Guid carritoId, Guid productoId)
        {
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            string urlEliminarProducto = _configuration.ObtenerEndPoint("deleteofCarrito");

            var respuesta = await cliente.DeleteAsync($"{urlEliminarProducto}/{carritoId}/producto/{productoId}");

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("/Carrito/Index");
            }

            ModelState.AddModelError(string.Empty, "Error al eliminar el producto del carrito.");
            return Page();
        }
    }
}
