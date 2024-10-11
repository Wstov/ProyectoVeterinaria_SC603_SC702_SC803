using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages.Cita
{
    [Authorize(Roles = "1,2,3")]
    public class RegistroCitaModel : PageModel
    {
        private Configuracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty] public Abstracciones.Modelos.Cita cita { get; set; } = default!;
        public RegistroCitaModel(Configuracion configuracion, IHttpClientFactory httpClientFactory) { _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> OnPost() {
            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("addCita");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var respuesta = await cliente.PostAsJsonAsync<Abstracciones.Modelos.Cita>(endPoint, cita);
            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("../Index");
            }
            return Page();
        }
        public async Task<ActionResult> OnGet()
        {
            return Page();
        }
    }
}
