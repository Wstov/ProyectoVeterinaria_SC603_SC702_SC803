using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Abstracciones.Modelos;

namespace WEB.Pages.Cita
{
    public class IndexModel : PageModel
    {
        private Configuracion _configuration;

        [BindProperty]  public IList<Abstracciones.Modelos.Cita> cita { get; set; } = default!;

        public IndexModel(Configuracion configuration)
        {
            _configuration = configuration;
        }

        public async Task<ActionResult> OnGet()
        {
            string urlEndPoint = _configuration.ObtenerEndPoint("getAllCita");
            var cliente = new HttpClient();

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint));

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

                    cita = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Cita>>(resultado, options);
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
