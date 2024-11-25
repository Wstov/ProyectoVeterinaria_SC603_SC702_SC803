using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Personas
{
    [Authorize(Roles = "1")] // Solo rol autorizado
    public class VeterinarioModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuracion;

        // Solo incluye la especialidad y el ID necesario para identificar al veterinario.
        [BindProperty]
        public Guid EmpleadoID { get; set; }
        [BindProperty]
        public Guid UsuarioID { get; set; }

        [BindProperty]
        public string Especialidad { get; set; } = string.Empty;

        public VeterinarioModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet(Guid id)
        {
            string urlEndPoint = _configuracion.ObtenerEndPoint("getVeterinario");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, id));

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

                    // Deserialize solo para obtener los datos necesarios.
                    var veterinario = JsonSerializer.Deserialize<Veterinario>(resultado, options);
                    if (veterinario != null)
                    {
                        EmpleadoID = veterinario.EmpleadoID;
                        UsuarioID= veterinario.UsuarioID;
                        Especialidad = veterinario.Especialidad;
                    }
                }
                catch (JsonException)
                {
                    return Page();
                }
            }
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("editVeterinario");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var payload = new
            {
                EmpleadoID,
                UsuarioID,
                Especialidad
            };

            var respuesta = await cliente.PutAsJsonAsync(endPoint, payload);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}