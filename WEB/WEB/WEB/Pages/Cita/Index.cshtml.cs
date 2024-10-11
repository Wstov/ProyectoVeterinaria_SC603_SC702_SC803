using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;

namespace WEB.Pages.Cita
{
    [Authorize(Roles = "1,3")]
    public class IndexModel : PageModel
    {
        private Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty]  public IList<Abstracciones.Modelos.Cita> cita { get; set; } = default!;

        public IndexModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet()
        {
            string urlEndPoint = _configuration.ObtenerEndPoint("getAllCita");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

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

                    cita = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Cita>>(resultado, options);
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
