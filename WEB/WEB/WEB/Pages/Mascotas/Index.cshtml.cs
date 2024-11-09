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

        public class MascotaConPropietarioYExpedientes
        {
            public Abstracciones.Modelos.Mascotas Mascota { get; set; }
            public string NombrePropietario { get; set; }
            public IList<Expediente> Expedientes { get; set; } = new List<Expediente>();
        }

        [BindProperty] public IList<Abstracciones.Modelos.Mascotas> mascotas { get; set; } = default!;
        [BindProperty] public Persona persona { get; set; } = default!;
        [BindProperty] public IList<MascotaConPropietarioYExpedientes> MascotasConPropietarioYExpedientes { get; set; } = new List<MascotaConPropietarioYExpedientes>();
        public string SearchTerm { get; set; }

        public IndexModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet(string? searchTerm)
        {
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
                    string urlEndPointPersona = _configuration.ObtenerEndPoint("ObtenerPersona");
                    var solicitudPersona = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPointPersona, mascota.UsuarioID));
                    var respuestaPersona = await cliente.SendAsync(solicitudPersona);

                    string nombrePropietario = "Propietario desconocido";
                    if (respuestaPersona.IsSuccessStatusCode)
                    {
                        var resultadoPersona = await respuestaPersona.Content.ReadAsStringAsync();
                        var persona = JsonSerializer.Deserialize<Persona>(resultadoPersona, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        nombrePropietario = persona?.Nombre ?? nombrePropietario;
                    }
                    string urlEndPointExpedientes = _configuration.ObtenerEndPoint("getExpedientesPorMascota");
                    var solicitudExpedientes = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPointExpedientes, mascota.MascotaID));
                    var respuestaExpedientes = await cliente.SendAsync(solicitudExpedientes);

                    IList<Expediente> expedientes = new List<Expediente>(); // Lista vacía por defecto
                    if (respuestaExpedientes.IsSuccessStatusCode)
                    {
                        var resultadoExpedientes = await respuestaExpedientes.Content.ReadAsStringAsync();
                        if (!string.IsNullOrWhiteSpace(resultadoExpedientes)) 
                        {
                            expedientes = JsonSerializer.Deserialize<List<Expediente>>(resultadoExpedientes, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        }
                    }

                    MascotasConPropietarioYExpedientes.Add(new MascotaConPropietarioYExpedientes
                    {
                        Mascota = mascota,
                        NombrePropietario = nombrePropietario,
                        Expedientes = expedientes
                    });
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    MascotasConPropietarioYExpedientes = MascotasConPropietarioYExpedientes
                        .Where(m => m.Mascota.NombreMascota.ToLower().Contains(searchTerm.ToLower()))
                        .ToList();
                }
            }

            return Page();
        }
    }

}
