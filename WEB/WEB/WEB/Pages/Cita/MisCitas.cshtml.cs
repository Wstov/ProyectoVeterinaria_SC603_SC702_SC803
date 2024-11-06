using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;

namespace WEB.Pages.Cita
{
    [Authorize(Roles = "1,2, 3")]
    public class MisCitasModel : PageModel
    {
        private readonly Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty] public IList<Abstracciones.Modelos.Cita> Citas { get; set; } = default!;
        public Dictionary<Guid, string> MascotasNombres { get; set; } = new Dictionary<Guid, string>();

        public MisCitasModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet()
        {
            var idUsuario = Abstracciones.Modelos.Autenticacion.ClaimsPrincipalExtensions.GetIdUsuario(User);
            if (idUsuario == Guid.Empty)
            {
                return RedirectToPage("/Error");
            }

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            // Obtener las citas
            string urlEndPointCitas = _configuration.ObtenerEndPoint("getCitas");
            var requestUrlCitas = $"{urlEndPointCitas}?PersonaID={idUsuario}";
            var solicitudCitas = new HttpRequestMessage(HttpMethod.Get, requestUrlCitas);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var respuestaCitas = await cliente.SendAsync(solicitudCitas);

            if (respuestaCitas.IsSuccessStatusCode)
            {
                var resultadoCitas = await respuestaCitas.Content.ReadAsStringAsync();

                if (!string.IsNullOrWhiteSpace(resultadoCitas))
                {
                  
                    Citas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Cita>>(resultadoCitas, options);
                }
                else
                {
                    Citas = new List<Abstracciones.Modelos.Cita>(); // Lista vacía si no hay citas
                }

                // Obtener las mascotas si hay citas
                string urlEndPointMascotas = _configuration.ObtenerEndPoint("MisMascotas");
                var requestUrlMascotas = $"{urlEndPointMascotas}?PersonaID={idUsuario}";
                var solicitudMascotas = new HttpRequestMessage(HttpMethod.Get, requestUrlMascotas);
                var respuestaMascotas = await cliente.SendAsync(solicitudMascotas);

                if (respuestaMascotas.IsSuccessStatusCode)
                {
                    var resultadoMascotas = await respuestaMascotas.Content.ReadAsStringAsync();

                    if (!string.IsNullOrWhiteSpace(resultadoMascotas))
                    {
                        var mascotas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Mascotas>>(resultadoMascotas, options);
                        MascotasNombres = mascotas?.ToDictionary(m => m.MascotaID, m => m.NombreMascota) ?? new Dictionary<Guid, string>();
                    }
                    else
                    {
                        MascotasNombres = new Dictionary<Guid, string>(); // Lista vacía si no hay mascotas
                    }
                }
            }
            else
            {
                Citas = new List<Abstracciones.Modelos.Cita>(); // Manejo de error: lista vacía si la solicitud de citas falla
            }

            return Page();
        }

    }

}

