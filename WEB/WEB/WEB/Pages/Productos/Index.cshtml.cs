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
        private Configuracion _configuration;

        public IList<Producto> producto { get; set; } = default!;
        public string SearchTerm { get; set; }

        public IndexModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet(string? searchTerm)
        {
            string urlEndPoint = _configuration.ObtenerEndPoint("getAllProducto");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
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
    }
}
