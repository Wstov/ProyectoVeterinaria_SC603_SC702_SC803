using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Mascotas
{
    [Authorize(Roles = "1,2,3")]
    public class addMascotaModel : PageModel
    {
        private Configuracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty] public Abstracciones.Modelos.Mascotas mascota { get; set; }
        public addMascotaModel(Configuracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ActionResult> OnPost()
        {

            string endPoint = _configuracion.ObtenerEndPoint("addMascota");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var respuesta = await cliente.PostAsJsonAsync<Abstracciones.Modelos.Mascotas>(endPoint, mascota);
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
