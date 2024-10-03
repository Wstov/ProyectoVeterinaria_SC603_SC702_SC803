using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Cita
    {
        public Guid CitaID { get; set; }
        public DateTime? FechaHora { get; set; }
        [Required(ErrorMessage = "El nombre del dueño es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del dueño debe ser mayor a 3 caracteres y menor a 50 carcacteres.")]
        public string? Dueno { get; set; }
        public Guid MascotaID { get; set; }
        [Required(ErrorMessage = "El motivo es obligatorio.")]
        [StringLength(100, MinimumLength = 7, ErrorMessage = "El nombre del dueño debe ser mayor a 7 caracteres y menor a 100 carcacteres.")]
        public string? Motivo { get; set; }
        public Guid VeterinarioAsignadoID { get; set; }
        public Guid PersonaID { get; set; }
    }
}
