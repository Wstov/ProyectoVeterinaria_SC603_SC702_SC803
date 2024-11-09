using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class Expediente
    {
        public Guid ExpedienteID { get; set; }
        public Guid MascotaID { get; set; }
        public string Diagnostico { get; set; }
        public DateTime FechaActu { get; set; }
    }
}
