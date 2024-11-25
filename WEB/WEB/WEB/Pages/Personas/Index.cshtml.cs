using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Personas
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuration;

        public IList<Persona> personas { get; set; } = default!;
        public IList<VeterinarioGet> veterinarios { get; set; } = default!;
        public string SearchTerm { get; set; }

        public IndexModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet()
        {
            // Obtener personas
            string urlEndPointPersonas = _configuration.ObtenerEndPoint("ObtenerPersonas");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var solicitudPersonas = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPointPersonas));
            var respuestaPersonas = await cliente.SendAsync(solicitudPersonas);

            if (respuestaPersonas.IsSuccessStatusCode)
            {
                var resultado = await respuestaPersonas.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                personas = JsonSerializer.Deserialize<List<Persona>>(resultado, options) ?? new List<Persona>();
            }
            string urlEndPointVeterinarios = _configuration.ObtenerEndPoint("getVeterinarios");
            var solicitudVeterinarios = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPointVeterinarios));
            var respuestaVeterinarios = await cliente.SendAsync(solicitudVeterinarios);

            if (respuestaVeterinarios.IsSuccessStatusCode)
            {
                var resultado = await respuestaVeterinarios.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(resultado)) 
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    veterinarios = JsonSerializer.Deserialize<List<VeterinarioGet>>(resultado, options) ?? new List<VeterinarioGet>();
                }
                else
                {
                    veterinarios = new List<VeterinarioGet>(); 
                }
            }

            return Page();
        }
    }
}
