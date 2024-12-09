using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Productos
{
    [Authorize(Roles = "1,2,3")]
    public class ComprarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuracion;

        [BindProperty] public Persona persona { get; set; } = default!;
        [BindProperty] public IList<Abstracciones.Modelos.Mascotas> mascotas { get; set; } = new List<Abstracciones.Modelos.Mascotas>();

        public ComprarModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("EditarPersona");

            var cliente = _httpClientFactory.CreateClient("ClienteFerreteria");

            var respuesta = await cliente.PutAsJsonAsync<Persona>(endPoint, persona);

            if (respuesta.IsSuccessStatusCode)
            {
                return Page();
            }

            return Page();
        }

        public async Task<ActionResult> OnGet(Guid id)
        {
            string urlPersonaEndPoint = _configuracion.ObtenerEndPoint("ObtenerPersona");

            // Cliente para realizar solicitudes HTTP
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            // Solicitar los datos de la persona
            var solicitudPersona = new HttpRequestMessage(HttpMethod.Get, string.Format(urlPersonaEndPoint, id));
            var respuestaPersona = await cliente.SendAsync(solicitudPersona);

            if (respuestaPersona.IsSuccessStatusCode)
            {
                var resultadoPersona = await respuestaPersona.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    persona = JsonSerializer.Deserialize<Persona>(resultadoPersona, options);
                }
                catch (JsonException)
                {
                    return Page();
                }
            }

           

            return Page();
        }
    }
}
