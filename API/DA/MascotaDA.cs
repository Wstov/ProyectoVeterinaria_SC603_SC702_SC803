using Abstracciones.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA
{
    public class MascotaDA : IMascotaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public MascotaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> Agregar(Mascota mascota)
        {
            string sql = @"addMascota"; 
            await _sqlConnection.QueryAsync<Mascota>(sql, new
            {
                UsuarioID = mascota.UsuarioID,
                NombreAnimal = mascota.NombreAnimal,
                NombreMascota = mascota.NombreMascota,
                FechaNacimiento = mascota.FechaNacimiento,
                Raza = mascota.Raza,
                Genero = mascota.Genero,
                Caracteristicas = mascota.Caracteristicas
            }, commandType: CommandType.StoredProcedure);

            return mascota.MascotaID;
        }

        public async Task<Guid> Editar(Mascota mascota)
        {
            string sql = @"editMascota";
            await _sqlConnection.QueryAsync<Mascota>(sql, new
            {
                MascotaID = mascota.MascotaID,
                NombreAnimal = mascota.NombreAnimal,
                NombreMascota = mascota.NombreMascota,
                FechaNacimiento = mascota.FechaNacimiento,
                Raza = mascota.Raza,
                Genero = mascota.Genero,
                Caracteristicas = mascota.Caracteristicas
            }, commandType: CommandType.StoredProcedure);

            return mascota.MascotaID;
        }

        public async Task<Guid> Eliminar(Guid MascotaID)
        {
            string sql = @"deleteMascota"; 
            await _sqlConnection.QueryAsync<Mascota>(sql, new { MascotaID }, commandType: CommandType.StoredProcedure);

            return MascotaID;
        }

        public async Task<IEnumerable<Mascota>> Obtener(Guid UsuarioID)
        {
            string sql = @"getMascotasUsuario";  
            var consulta = await _sqlConnection.QueryAsync<Mascota>(sql, new { UsuarioID }, commandType: CommandType.StoredProcedure);

            return consulta.Any() ? consulta : null;
        }

        public async Task<Mascota> ObtenerOne(Guid MascotaID)
        {
            string sql = @"getMascota"; 
            var consulta = await _sqlConnection.QueryAsync<Mascota>(sql, new { MascotaID }, commandType: CommandType.StoredProcedure);

            return consulta.FirstOrDefault();
        }

        public async Task<IEnumerable<Mascota>> ObtenerTodos()
        {
            string sql = @"getMascotas";
            var consulta = await _sqlConnection.QueryAsync<Mascota>(sql, commandType: CommandType.StoredProcedure);

            return consulta.Any() ? consulta : null;
        }
    }

}
