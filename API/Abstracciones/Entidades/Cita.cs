using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
    public class Cita
    {
        public Guid CitaID { get; set; }
        public DateTime? FechaHora { get; set; }
        public string? Dueno { get; set; }
        public Guid MascotaID { get; set; }
        public string? Motivo { get; set; }
        public Guid VeterinarioAsignadoID { get; set; }
        public Guid PersonaID { get; set; }

    }
}
