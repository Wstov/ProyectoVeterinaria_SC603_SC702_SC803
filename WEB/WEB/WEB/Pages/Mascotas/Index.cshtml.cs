using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Mascotas
{
    [Authorize(Roles = "1,3")]
    public class IndexModel : PageModel
    {

        private readonly Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public class MascotaConPropietario
        {
            public Abstracciones.Modelos.Mascotas Mascota { get; set; }
            public string NombrePropietario { get; set; }
        }
        [BindProperty] public IList<Abstracciones.Modelos.Mascotas> mascotas { get; set; } = default!;
        [BindProperty] public Persona persona { get; set; } = default!;
        public string SearchTerm { get; set; }

        public IndexModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty] public IList<MascotaConPropietario> MascotasConPropietario { get; set; } = new List<MascotaConPropietario>();

        public async Task<ActionResult> OnGet(string? searchTerm)
        {
            // Obtener la lista de mascotas
            string urlEndPointMascotas = _configuration.ObtenerEndPoint("allMascotas");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var solicitudMascotas = new HttpRequestMessage(HttpMethod.Get, urlEndPointMascotas);
            var respuestaMascotas = await cliente.SendAsync(solicitudMascotas);

            if (respuestaMascotas.IsSuccessStatusCode)
            {
                var resultadoMascotas = await respuestaMascotas.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    mascotas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Mascotas>>(resultadoMascotas, options);
                }
                catch (JsonException ex)
                {
                    return Page();
                }

                foreach (var mascota in mascotas)
                {
                    // Obtener el propietario de cada mascota
                    string urlEndPointPersona = _configuration.ObtenerEndPoint("ObtenerPersona");
                    var solicitudPersona = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPointPersona, mascota.UsuarioID));
                    var respuestaPersona = await cliente.SendAsync(solicitudPersona);

                    if (respuestaPersona.IsSuccessStatusCode)
                    {
                        var resultadoPersona = await respuestaPersona.Content.ReadAsStringAsync();
                        var persona = JsonSerializer.Deserialize<Persona>(resultadoPersona, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        // Crear objeto MascotaConPropietario
                        MascotasConPropietario.Add(new MascotaConPropietario
                        {
                            Mascota = mascota,
                            NombrePropietario = persona?.Nombre ?? "Propietario desconocido"
                        });
                    }
                }

                // Filtrar por término de búsqueda si es necesario
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    MascotasConPropietario = MascotasConPropietario
                        .Where(m => m.Mascota.NombreMascota.ToLower().Contains(searchTerm.ToLower()))
                        .ToList();
                }
            }

            return Page();
        }

    }
}