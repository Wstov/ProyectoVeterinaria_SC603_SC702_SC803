using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json;

namespace WEB.Pages.Cita
{
    [Authorize(Roles = "1,2, 3")]
    public class MisCitasModel : PageModel
    {
        private Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty] public IList<Abstracciones.Modelos.Cita> cita { get; set; } = default!;
        public MisCitasModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> OnGet()
        {
            var idUsuario = new Guid();
            if(User.Identity.IsAuthenticated)

            {
                idUsuario = Abstracciones.Modelos.Autenticacion.ClaimsPrincipalExtensions.GetIdUsuario(User);
            }

            string urlEndPoint = _configuration.ObtenerEndPoint("getCitas");
            var requestUrl = $"{urlEndPoint}?PersonaID={idUsuario}";
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, requestUrl);

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

