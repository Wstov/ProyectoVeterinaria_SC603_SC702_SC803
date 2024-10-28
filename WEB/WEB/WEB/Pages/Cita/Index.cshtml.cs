using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WEB.Pages.Cita
{
    [Authorize(Roles = "1,3")]
    public class IndexModel : PageModel
    {
        private readonly Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty] public IList<Abstracciones.Modelos.Cita> Citas { get; set; } = default!;
        public Dictionary<Guid, (string Nombre, string Tipo)> MascotasInfo { get; set; } = new Dictionary<Guid, (string, string)>();

        public IndexModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet()
        {
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            // Obtener todas las citas
            string urlEndPointCitas = _configuration.ObtenerEndPoint("getAllCita");
            var solicitudCitas = new HttpRequestMessage(HttpMethod.Get, urlEndPointCitas);
            var respuestaCitas = await cliente.SendAsync(solicitudCitas);

            if (respuestaCitas.IsSuccessStatusCode)
            {
                var resultadoCitas = await respuestaCitas.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Citas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Cita>>(resultadoCitas, options);
            }

            // Obtener todos los datos de las mascotas
            string urlEndPointMascotas = _configuration.ObtenerEndPoint("allMascotas");
            var solicitudMascotas = new HttpRequestMessage(HttpMethod.Get, urlEndPointMascotas);
            var respuestaMascotas = await cliente.SendAsync(solicitudMascotas);

            if (respuestaMascotas.IsSuccessStatusCode)
            {
                var resultadoMascotas = await respuestaMascotas.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var mascotas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Mascotas>>(resultadoMascotas, options);

                if (mascotas != null)
                {
                    MascotasInfo = mascotas.ToDictionary(m => m.MascotaID, m => (m.NombreMascota, m.NombreAnimal));
                }
            }

            return Page();
        }
    }

}
