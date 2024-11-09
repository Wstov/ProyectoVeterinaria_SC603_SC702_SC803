using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos

{
    public class Expediente
    {
        [Required]
        public Guid ExpedienteID { get; set; }
        [Required]
        public Guid MascotaID { get; set; }
        [Required(ErrorMessage = "El Diagnostico es obligatorio.")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "El Diagnostico debe ser mayor a 5 caracteres y menor a 200 carcacteres.")]
        public string? Diagnostico { get; set; }
        [Required]
        public DateTime FechaActu { get; set; }

    }
}
