using BC;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace WEB.Pages.Productos
{

    public class PedidosModel : PageModel
    {
        private readonly Configuracion _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty] public IList<Abstracciones.Modelos.Factura> Facturas { get; set; } = default!;
        public Dictionary<Guid, string> DuenosInfo { get; set; } = new Dictionary<Guid, string>();
        public Dictionary<Guid, string> ProductosInfo { get; set; } = new Dictionary<Guid, string>();
        public PedidosModel(Configuracion configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ActionResult> OnGet()
        {
            var cliente = _httpClientFactory.CreateClient("ClienteVeterinaria");

            // Obtener todas las facturas
            string urlEndPointFacturas = _configuration.ObtenerEndPoint("getFacturas");
            var solicitudFacturas = new HttpRequestMessage(HttpMethod.Get, urlEndPointFacturas);
            var respuestaFacturas = await cliente.SendAsync(solicitudFacturas);

            if (respuestaFacturas.IsSuccessStatusCode)
            {
                var resultadoFacturas = await respuestaFacturas.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Facturas = JsonSerializer.Deserialize<List<Abstracciones.Modelos.Factura>>(resultadoFacturas, options);
            }

            foreach (var factura in Facturas)
            {
               
                    // Obtener información del dueño (IdPersona) si no está en el diccionario
                    if (!DuenosInfo.ContainsKey(factura.IdPersona))
                    {
                        string urlEndPointPersona = string.Format(_configuration.ObtenerEndPoint("ObtenerPersona"), factura.IdPersona);
                        var solicitudPersona = new HttpRequestMessage(HttpMethod.Get, urlEndPointPersona);
                        var respuestaPersona = await cliente.SendAsync(solicitudPersona);

                        if (respuestaPersona.IsSuccessStatusCode)
                        {
                            var resultadoPersona = await respuestaPersona.Content.ReadAsStringAsync();
                            var persona = JsonSerializer.Deserialize<Abstracciones.Modelos.Persona.Persona>(resultadoPersona, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                            if (persona != null)
                            {
                                DuenosInfo[factura.IdPersona] = persona.Nombre;
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Error al obtener la persona: {respuestaPersona.StatusCode}");
                        }
                    }
                
                string urlEndPointCarrito = $"https://localhost:7184/api/Carrito/finalizados/{factura.IdCarrito}";
                var solicitudCarrito = new HttpRequestMessage(HttpMethod.Get, urlEndPointCarrito);
                var respuestaCarrito = await cliente.SendAsync(solicitudCarrito);

                if (respuestaCarrito.IsSuccessStatusCode)
                {
                    var resultadoCarrito = await respuestaCarrito.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var detallesCarrito = JsonSerializer.Deserialize<List<Abstracciones.Modelos.DetallesCarrito>>(resultadoCarrito, options);

                    foreach (var detalle in detallesCarrito)
                    {
                        if (detalle.ProductoID == Guid.Empty)
                        {
                            Console.WriteLine("ProductoID inválido en los detalles del carrito.");
                            continue; // Saltar detalles inválidos
                        }

                        if (!ProductosInfo.ContainsKey(detalle.ProductoID))
                        {
                            string urlEndPointProducto = string.Format("https://localhost:7184/api/Producto?ProductoID={0}", detalle.ProductoID);
                            var solicitudProducto = new HttpRequestMessage(HttpMethod.Get, urlEndPointProducto);
                            var respuestaProducto = await cliente.SendAsync(solicitudProducto);

                            if (respuestaProducto.IsSuccessStatusCode)
                            {
                                var resultadoProducto = await respuestaProducto.Content.ReadAsStringAsync();
                                if (!string.IsNullOrWhiteSpace(resultadoProducto))
                                {
                                    var producto = JsonSerializer.Deserialize<Abstracciones.Modelos.Producto>(resultadoProducto, options);
                                    if (producto != null)
                                    {
                                        ProductosInfo[detalle.ProductoID] = producto.Nombre;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Error al obtener el producto: {respuestaProducto.StatusCode}");
                            }
                        }

                        if (ProductosInfo.ContainsKey(detalle.ProductoID))
                        {
                            factura.ProductosDetalles ??= new List<string>();
                            factura.ProductosDetalles.Add($"{ProductosInfo[detalle.ProductoID]} (Cantidad: {detalle.Cantidad})");
                        }
                    }
                }
            }


            return Page();
        }
    }


}
