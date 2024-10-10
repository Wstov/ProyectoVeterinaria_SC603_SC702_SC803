using Abstracciones.Modelos.Autenticacion;
using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace WEB.Pages.Cuenta
{
    public class RegistrarseModel : PageModel
    {
        [BindProperty] public Registro registro { get; set; } = default!;

        private readonly Configuracion _configuracion;

        public RegistrarseModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (registro.Password != registro.ConfirmPassword)
            {
                ModelState.AddModelError("registro.ConfirmPassword", "Las contrase�as no coinciden.");
                return Page();
            }

            // Generar el hash de la contrase�a
            var hash = Password.GenerarHash(registro.Password);
            registro.Hash = Password.ObtenerHash(hash);

            string endPoint = _configuracion.ObtenerEndPoint("Registro");

            using var cliente = new HttpClient();
            cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var respuesta = await cliente.PostAsJsonAsync(endPoint, registro);

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError("registro", "Ocurri� un error en el servidor. Int�ntalo m�s tarde.");
                return Page();
            }

            return RedirectToPage("./Login", new { registro.Correo });
        }
    }
}
