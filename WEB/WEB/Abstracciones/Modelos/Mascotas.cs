using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
   public class Mascotas
{
    [Required]
    public Guid MascotaID { get; set; }

    [Required]
    public Guid UsuarioID { get; set; }

    [Required(ErrorMessage = "El nombre del animal es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre del animal debe ser mayor a 3 caracteres y menor a 50 caracteres.")]
    public string? NombreAnimal { get; set; }

    [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre de la mascota debe ser mayor a 3 caracteres y menor a 50 caracteres.")]
    public string? NombreMascota { get; set; }

    [Required(ErrorMessage = "La fecha y hora de nacimiento es obligatoria.")]
    [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy HH:mm}", ApplyFormatInEditMode = true)]
    [DataType(DataType.DateTime)]
    [CustomValidation(typeof(Mascotas), nameof(FechaNacimientoValidation))]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "La raza de la mascota es obligatoria.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "La raza de la mascota debe ser mayor a 3 caracteres y menor a 50 caracteres.")]
    public string? Raza { get; set; }

    [Required(ErrorMessage = "El género de la mascota es obligatorio.")]
    [RegularExpression(@"^[FfMm]$", ErrorMessage = "El género de la mascota debe ser 'F', 'f', 'M' o 'm'.")]
    public string? Genero { get; set; }

    [Required(ErrorMessage = "Debe agregar las características de la mascota.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Las características de la mascota deben ser mayor a 5 caracteres y menor a 50 caracteres.")]
    public string? Caracteristicas { get; set; }

    // Método de validación personalizado para la fecha
    public static ValidationResult FechaNacimientoValidation(DateTime fechaNacimiento, ValidationContext context)
    {
        if (fechaNacimiento.Date >= DateTime.Today)
        {
            return new ValidationResult("La fecha de nacimiento no puede ser hoy o una fecha futura.");
        }

        return ValidationResult.Success;
    }
}
}
