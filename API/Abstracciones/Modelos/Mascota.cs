using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Mascota
    {
        [Required]
        public Guid MascotaID { get; set; }
        [Required]
        public Guid UsuarioID { get; set; }
        [Required(ErrorMessage = "El nombre del animal es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del animal debe ser mayor a 3 caracteres y menor a 50 carcacteres.")]
        public string? NombreAnimal { get; set; }
        [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la mascota debe ser mayor a 3 caracteres y menor a 50 carcacteres.")]
        public string? NombreMascota { get; set; }
        [Required(ErrorMessage = "La fecha y hora es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "La raza de la mascota es obligatorio.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La raza de la mascota debe ser mayor a 3 caracteres y menor a 50 carcacteres.")]
        public string? Raza { get; set; }
        [Required(ErrorMessage = "El género de la mascota es obligatorio.")]
        [RegularExpression(@"^[FfMm]$", ErrorMessage = "El género de la mascota debe ser 'F', 'f', 'M' o 'm'.")]
        public string? Genero { get; set; }
        [Required(ErrorMessage = "Debe agregar las carateristicas de la mascota.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Las carateristicas de la mascota deben ser mayor a 5 caracteres y menor a 50 carcacteres.")]
        public string? Caracteristicas { get; set; }
    }
}
