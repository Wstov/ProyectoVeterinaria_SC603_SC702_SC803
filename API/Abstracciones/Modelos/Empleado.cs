using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Empleado
    {
     
    public Guid EmpleadoID { get; set; }
        public Guid UsuarioID { get; set; }
        public string Especialidad { get; set; }
    
}
}
