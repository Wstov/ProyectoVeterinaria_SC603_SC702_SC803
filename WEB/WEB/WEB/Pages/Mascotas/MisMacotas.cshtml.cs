using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Mascotas
{
    public class MisMacotasModel : PageModel
    {
        private Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty] public IList<Abstracciones.Modelos.Mascotas> mascotas { get; set; } = default!;
        public MisMacotasModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> OnGet()
        {
            var idUsuario = new Guid();
            if (User.Identity.IsAuthenticated)

            {
                idUsuario = Abstracciones.Modelos.Autenticacion.ClaimsPrincipalExtensions.GetIdUsuario(User);
            }

            string urlEndPoint = _configuration.ObtenerEndPoint("MisMascotas");
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

                    mascotas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Mascotas>>(resultado, options);
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
