using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class Empleado
    {
        public Guid EmpleadoID { get; set; }
        public Guid UsuarioID { get; set; }
        public string Especialidad { get; set; }
    }
}
