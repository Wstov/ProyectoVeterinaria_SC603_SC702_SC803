using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WEB.Pages.Cita
{
    [Authorize(Roles = "1,2,3")]
    public class RegistroCitaModel : PageModel
    {
        private Configuracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;
        [BindProperty] public Abstracciones.Modelos.Cita cita { get; set; } = new Abstracciones.Modelos.Cita();
        [BindProperty] public Persona persona { get; set; } = default!;
        public RegistroCitaModel(Configuracion configuracion, IHttpClientFactory httpClientFactory) { _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }
      
        public async Task<ActionResult> OnPost() {
            
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
            var idUsuario = Abstracciones.Modelos.Autenticacion.ClaimsPrincipalExtensions.GetIdUsuario(User);

            if (idUsuario == Guid.Empty)
            {
                return RedirectToPage("/Error");
            }

            string urlEndPoint = _configuracion.ObtenerEndPoint("ObtenerPersona");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPoint, idUsuario));
            var respuesta = await cliente.SendAsync(solicitud);

            if (respuesta.IsSuccessStatusCode)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                try
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    persona = JsonSerializer.Deserialize<Persona>(resultado, options);

                    if (persona != null)
                    {
                    
                        cita.Dueno = persona.Nombre;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "No se pudo obtener la información de la persona.");
                    }
                }
                catch (JsonException ex)
                {
                    ModelState.AddModelError(string.Empty, "Error al procesar la información de la persona: " + ex.Message);
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al obtener la persona.");
            }

            return Page();
        }





    }
}
