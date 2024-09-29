using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BC
{
    public class Configuracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerEndPoint(string NombreEndPoint)
        {
            return _configuration.GetSection("APIEndPoints").Get<List<APIEndPoint>>()
                .Where(endPoint => endPoint.Nombre == NombreEndPoint)
                .First().Valor;
        }
    }
}
