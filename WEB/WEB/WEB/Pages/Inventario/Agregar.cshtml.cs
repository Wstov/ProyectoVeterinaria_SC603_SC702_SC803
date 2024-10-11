using BC;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages.Inventario
{
    [Authorize(Roles = "1")]
    public class AgregarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private Configuracion _configuracion;

        [BindProperty] public Producto producto { get; set; } = default!;

        public IFormFile imagen;

        public AgregarModel(Configuracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }
      
        public async Task<ActionResult> OnPost(IFormFile? imagen)
        {
            if (imagen != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imagen.CopyToAsync(memoryStream);
                    producto.Imagen = memoryStream.ToArray();
                }
            }

            if (!ModelState.IsValid)
                return Page();

            string endPoint = _configuracion.ObtenerEndPoint("addProducto");

            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            var respuesta = await cliente.PostAsJsonAsync<Producto>(endPoint, producto);

            if (respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<ActionResult> OnGet()
        {
            return Page();
        }
    }
}

