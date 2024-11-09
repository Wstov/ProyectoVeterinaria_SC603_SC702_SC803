using Abstracciones.Modelos;
using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WEB.Pages.Mascotas
{
    [Authorize(Roles = "1,2,3")]
    public class addExpedienteModel : PageModel
    {
        private readonly Configuracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public Expediente Expediente { get; set; }

        public addExpedienteModel(Configuracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet(Guid mascotaID)
        {
            Expediente = new Expediente { MascotaID = mascotaID };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("addExpediente");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var respuesta = await cliente.PostAsJsonAsync(endPoint, Expediente);
            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError(string.Empty, "Error al agregar el expediente.");
            return Page();
        }
    }
}