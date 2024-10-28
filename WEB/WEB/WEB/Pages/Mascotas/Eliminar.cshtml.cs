using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Mascotas
{
    public class EliminarModel : PageModel
    {
      
        private readonly Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public Abstracciones.Modelos.Mascotas mascota { get; set; } = default!;

        public EliminarModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet(Guid MascotaID)
        {
            string urlEndPoint = _configuration.ObtenerEndPoint("Mascota");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, MascotaID));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                mascota = JsonSerializer.Deserialize<Abstracciones.Modelos.Mascotas>(resultado, options);
            }

            return Page();
        }
        public async Task<ActionResult> OnPost(Guid MascotaID)
        {


            string endPoint = _configuration.ObtenerEndPoint("eliminarMascota");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var solicitud = new HttpRequestMessage(HttpMethod.Delete, string.Format(endPoint, MascotaID));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("../Index");
            }
            else
            {
                return Page();
            }
        }

    }
}

