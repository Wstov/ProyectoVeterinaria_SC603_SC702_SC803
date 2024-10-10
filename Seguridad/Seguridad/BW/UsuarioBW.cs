using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Entidades;

namespace BW
{
    public class UsuarioBW : IUsuarioBW
    {
        private IUsuarioDA _usuarioDA;

        public UsuarioBW(IUsuarioDA usuarioDA)
        {
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> CrearUsuario(UsuarioPost usuario)
        {
            return await _usuarioDA.CrearUsuario(usuario);
        }

        public async Task<Usuario> ObtenerUsuario(Usuario usuario)
        {
            return await _usuarioDA.ObtenerUsuario(usuario);
        }
    }
}
