using Abstracciones.Modelos;
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

        public class MascotaConExpedientes
        {
            public Abstracciones.Modelos.Mascotas Mascota { get; set; }
            public IList<Expediente> Expedientes { get; set; } = new List<Expediente>();
        }

        [BindProperty]
        public IList<MascotaConExpedientes> MascotasConExpedientes { get; set; } = new List<MascotaConExpedientes>();

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

            // Define la lista de mascotas localmente
            List<Abstracciones.Modelos.Mascotas> mascotas = new List<Abstracciones.Modelos.Mascotas>();

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    mascotas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Mascotas>>(resultado, options) ?? new List<Abstracciones.Modelos.Mascotas>();

                    foreach (var mascota in mascotas)
                    {
                        var urlExpedientes = _configuration.ObtenerEndPoint("getExpedientesPorMascota");
                        var solicitudExpedientes = new HttpRequestMessage(HttpMethod.Get, string.Format(urlExpedientes, mascota.MascotaID));
                        var respuestaExpedientes = await cliente.SendAsync(solicitudExpedientes);

                        IList<Expediente> expedientes = new List<Expediente>();
                        if (respuestaExpedientes.IsSuccessStatusCode)
                        {
                            var resultadoExpedientes = await respuestaExpedientes.Content.ReadAsStringAsync();

                            if (!string.IsNullOrWhiteSpace(resultadoExpedientes))
                            {
                                expedientes = JsonSerializer.Deserialize<List<Expediente>>(resultadoExpedientes, options) ?? new List<Expediente>();
                            }
                        }

                        MascotasConExpedientes.Add(new MascotaConExpedientes
                        {
                            Mascota = mascota,
                            Expedientes = expedientes
                        });
                    }
                }
            }

            return Page();
        }
    }
}

