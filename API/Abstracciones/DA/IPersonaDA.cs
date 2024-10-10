﻿using Abstracciones.Modelos.Persona;

namespace Abstracciones.DA
{
    public interface IPersonaDA
    {
        public Task<Persona> Obtener(Guid Id);
        public Task<IEnumerable<Persona>> ObtenerTodos();
        public Task<bool> Agregar(PersonaPost persona);
        public Task<bool> Editar(Persona persona);
        public Task<bool> Eliminar(Guid Id);
    }
}