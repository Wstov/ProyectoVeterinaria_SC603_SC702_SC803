using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Empleados
{
    [Authorize(Roles = "1")] // Rol 1 autorizado
    public class EditarVeterinarioModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuracion;

        [BindProperty] public Veterinario Veterinario { get; set; } = default!;

        public EditarVeterinarioModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet(Guid empleadoID)
        {
            string urlEndPoint = _configuracion.ObtenerEndPoint("getVeterinario");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, empleadoID));

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

                    Veterinario = JsonSerializer.Deserialize<Veterinario>(resultado, options);
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

            var respuesta = await cliente.PutAsJsonAsync<Veterinario>(endPoint, Veterinario);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            return Page();
        }
    }
}
