using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Inventario
{
    public class ListarProductosModel : PageModel
    {
       
        private Configuracion _configuration;

        public IList<Producto> producto { get; set; } = default!;

        public ListarProductosModel(Configuracion configuration)
        {
            _configuration = configuration;
        }

        public async Task<ActionResult> OnGet()
        {
            string urlEndPoint = _configuration.ObtenerEndPoint("getAllProducto");

            var cliente = new HttpClient();

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    producto = JsonSerializer.Deserialize<List<Producto>>(resultado, options);
                }
                catch (JsonException ex)
                {
                    return Page();
                }
            }
            return Page();
        }
    }
}
