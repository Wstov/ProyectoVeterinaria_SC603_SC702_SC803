using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace WEB.Pages.Inventario
{
    [Authorize(Roles = "1")]
    public class EliminarModel : PageModel
    {
        private Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty] public Producto producto { get; set; } = default!;

        public EliminarModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> OnPost(Guid ProductoID)
        {
           

            string endPoint = _configuration.ObtenerEndPoint("deleteProducto");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endPoint, ProductoID));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("../Index");
            }
            else
            {
                TempData["Atenci�n"] = "Este producto tiene pedidos pendientes y un inventario activo. No puedes eliminarlo.";
                return Page();
            }
        }

        public async Task<ActionResult> OnGet(Guid ProductoID)
        {
            string urlEndPoint = _configuration.ObtenerEndPoint("getOneProducto");

            var cliente = new HttpClient();

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
