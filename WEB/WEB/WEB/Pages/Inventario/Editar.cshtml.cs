using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Inventario
{
    public class EditarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuration;
        [BindProperty] public Producto producto { get; set; } = default!;
        [BindProperty] public IFormFile? Imagen { get; set; }
        public EditarModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> OnPostAsync()
        {
            if (Imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await Imagen.CopyToAsync(memoryStream);
                    producto.Imagen = memoryStream.ToArray();
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endPoint = _configuration.ObtenerEndPoint("editProducto");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var respuesta = await cliente.PutAsJsonAsync<Producto>(endPoint, producto);
            respuesta.EnsureSuccessStatusCode();
            await respuesta.Content.ReadAsStringAsync();

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<ActionResult> OnGet(Guid ProductoID)
        {

            string urlEndPoint = _configuration.ObtenerEndPoint("getOneProducto");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, ProductoID));

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

                    producto = JsonSerializer.Deserialize<Producto>(resultado, options);
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
