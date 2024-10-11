using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Cita
{
    [Authorize(Roles = "1,3")]
    public class EditarModel : PageModel
    {

        private Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty] public Abstracciones.Modelos.Cita cita { get; set; } = default!;

        public EditarModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endPoint = _configuration.ObtenerEndPoint("editCita");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var respuesta = await cliente.PutAsJsonAsync<Abstracciones.Modelos.Cita>(endPoint, cita);
            respuesta.EnsureSuccessStatusCode();
            await respuesta.Content.ReadAsStringAsync();

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
        }

        public async Task<ActionResult> OnGet(Guid CitaID)
        {

            string urlEndPoint = _configuration.ObtenerEndPoint("getOneCita");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, CitaID));

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

                    cita = JsonSerializer.Deserialize<Abstracciones.Modelos.Cita>(resultado, options);
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
