﻿using Abstracciones.Entidades;

namespace Abstracciones.BW
{
    public interface IUsuarioBW
    {
        Task<Guid> CrearUsuario(UsuarioPost usuario);
        Task<Usuario> ObtenerUsuario(Usuario usuario);
    }
}