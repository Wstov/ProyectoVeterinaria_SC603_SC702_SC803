using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Abstracciones.Modelos;
namespace WEB.Pages.Mascotas
{

    [Authorize(Roles = "1,2,3")]
    public class EditarModel : PageModel
    {
        private readonly Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public Abstracciones.Modelos.Mascotas mascota { get; set; } = default!;

        public EditarModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet(Guid MascotaID)
        {
            string urlEndPoint = _configuration.ObtenerEndPoint("Mascota");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, MascotaID));

            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                mascota = JsonSerializer.Deserialize<Abstracciones.Modelos.Mascotas>(resultado, options);
            }

            return Page();
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endPoint = _configuration.ObtenerEndPoint("editMascota");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var respuesta = await cliente.PutAsJsonAsync<Abstracciones.Modelos.Mascotas>(endPoint, mascota);
            respuesta.EnsureSuccessStatusCode();
            await respuesta.Content.ReadAsStringAsync();

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("../Index");
            }
            else
            {
                return Page();
            }
        }
    }
}