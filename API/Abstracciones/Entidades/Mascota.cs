using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entidades
{
	public class Mascota
	{
		public Guid MascotaID { get; set; }
		public Guid UsuarioID { get; set; }
		public string? NombreAnimal { get; set; }
		public string? NombreMascota { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public string? Raza { get; set; }
		public string? Genero { get; set; }
		public string? Caracteristicas { get; set; }
	}

}
