using Abstracciones.Modelos;
using Abstracciones.Modelos.Persona;
using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
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
        public IList<Abstracciones.Modelos.Mascotas> Mascotas { get; set; } = new List<Abstracciones.Modelos.Mascotas>();

        public RegistroCitaModel(Configuracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet()
        {
            var idUsuario = Abstracciones.Modelos.Autenticacion.ClaimsPrincipalExtensions.GetIdUsuario(User);

            if (idUsuario == Guid.Empty)
            {
                return RedirectToPage("/Error");
            }

            // Cargar la información de la persona
            string urlEndPointPersona = _configuracion.ObtenerEndPoint("ObtenerPersona");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var solicitudPersona = new HttpRequestMessage(HttpMethod.Get, string.Format(urlEndPointPersona, idUsuario));
            var respuestaPersona = await cliente.SendAsync(solicitudPersona);

            if (respuestaPersona.IsSuccessStatusCode)
            {
                var resultadoPersona = await respuestaPersona.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                persona = JsonSerializer.Deserialize<Persona>(resultadoPersona, options);

                if (persona != null)
                {
                    cita.Dueno = persona.Nombre;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No se pudo obtener la información de la persona.");
                }
            }

            // Cargar la lista de mascotas
            string urlEndPointMascotas = _configuracion.ObtenerEndPoint("MisMascotas");
            var requestUrlMascotas = $"{urlEndPointMascotas}?PersonaID={idUsuario}";
            var solicitudMascotas = new HttpRequestMessage(HttpMethod.Get, requestUrlMascotas);
            var respuestaMascotas = await cliente.SendAsync(solicitudMascotas);

            if (respuestaMascotas.IsSuccessStatusCode)
            {
                var resultadoMascotas = await respuestaMascotas.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Mascotas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Mascotas>>(resultadoMascotas, options);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al obtener las mascotas.");
            }

            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            string endPoint = _configuracion.ObtenerEndPoint("addCita");
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");
            var respuesta = await cliente.PostAsJsonAsync<Abstracciones.Modelos.Cita>(endPoint, cita);
            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("../Index");
            }
            return Page();
        }
    }

}
