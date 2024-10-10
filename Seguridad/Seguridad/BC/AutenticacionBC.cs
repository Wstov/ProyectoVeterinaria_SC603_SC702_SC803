using Abstracciones.BC;
using Abstracciones.DA;
using Abstracciones.Entidades;
using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BC
{
    public class AutenticacionBC : IAutenticacionBC
    {
        public IConfiguration _configuration;
        public IUsuarioDA _usuarioDA;

        public AutenticacionBC(IConfiguration configuration, IUsuarioDA usuarioDA)
        {
            _configuration = configuration;
            _usuarioDA = usuarioDA;
        }

        public async Task<Token> LoginAsync(Login login)
        {
            Token respuestaToken = new Token()
            {
                AccessToken = string.Empty,
                ValidacionExitosa = false
            };

            var resultadoCredencialesVerificadas = await VerificarLogin(login);

            if (!resultadoCredencialesVerificadas)
                return respuestaToken;

            TokenConfiguracion tokenConfiguracion = _configuration.GetSection("Token").Get<TokenConfiguracion>();

            JwtSecurityToken token = await GenerarTokenJWT(login, tokenConfiguracion);

            respuestaToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

            respuestaToken.ValidacionExitosa = true;

            return respuestaToken;
        }

        private async Task<bool> VerificarLogin(Login login)
        {
            var construir = new Usuario { Corrreo = login.Correo };

            var usuario = await _usuarioDA.ObtenerUsuario(construir);

            if(usuario != null)
            {
                login.Id = usuario.IdPersona;
                return (login != null && login.Hash == usuario.Hash);
            }

            return false;
        }

        private async Task<JwtSecurityToken> GenerarTokenJWT(Login login, TokenConfiguracion tokenConfiguracion)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguracion.Key));

            List<Claim> claims = await GenerarClaims(login);

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var generateToken = new JwtSecurityToken(tokenConfiguracion.Issuer, tokenConfiguracion.Audience, claims,
                expires: DateTime.Now.AddMinutes(tokenConfiguracion.Expires), signingCredentials: credentials);

            return generateToken;
        }

        private async Task<List<Claim>> GenerarClaims(Login login)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("usuario", login.Correo));
            claims.Add(new Claim("IdUsuario", login.Id.ToString()));

            var perfiles = await ObtenerPerfiles(login);

            foreach (var perfil in perfiles)
            {
                claims.Add(new Claim(ClaimTypes.Role, perfil.Id.ToString()));
                claims.Add(new Claim("NombreRol", perfil.Tipo.ToString()));
            }

            return claims;
        }

        private async Task<IEnumerable<Rol>> ObtenerPerfiles(Login login)
        {
            return await _usuarioDA.ObtenerPerfilesxUsuario(new Usuario
            {
                Corrreo = login.Correo
            });
        }
    }
}