using Abstracciones.Entidades;

namespace Abstracciones.DA
{
    public interface IUsuarioDA
    {
        Task<Usuario> ObtenerUsuario(Usuario usuario);
        Task<IEnumerable<Rol>> ObtenerPerfilesxUsuario(Usuario usuario);
        Task<Guid> CrearUsuario(UsuarioPost usuario);
    }
}