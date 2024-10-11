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
    public class CitaDA: ICitaDA
    {
        private IRepositorioDapper _repositorioDapper;

        private SqlConnection _sqlConnection;

        public CitaDA(IRepositorioDapper repositorioDapper) {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> Agregar(Cita cita)
        {
            string sql = @"addCita";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.Cita>(sql, new
            {
                FechaHora = cita.FechaHora,
                Dueno = cita.Dueno,
                MascotaID = cita.MascotaID,
                Motivo = cita.Motivo,
                VeterinarioAsignadoID = cita.VeterinarioAsignadoID,
                PersonaID = cita.PersonaID,
            });
            return cita.CitaID;
        }

        public async Task<Guid> Editar(Cita cita)
        {
            string sql = @"editCita";
            await _sqlConnection.QueryAsync<Abstracciones.Entidades.Cita>(sql, new
            {
                CitaID = cita.CitaID,
                FechaHora = cita.FechaHora,
                Dueno = cita.Dueno,
                MascotaID = cita.MascotaID,
                Motivo = cita.Motivo,
                VeterinarioAsignadoID = cita.VeterinarioAsignadoID,
                PersonaID = cita.PersonaID,
            });
            return cita.CitaID;
        }

        public async Task<Guid> Eliminar(Guid CitaID)
        {
            string sql = @"deleteCitas";
            var consulta = await _sqlConnection.QueryAsync<Cita>(sql, new { CitaID = CitaID });

            return CitaID;
        }

        public async Task<IEnumerable<Cita>> Obtener(Guid PersonaID)
        {
            string sql = @"getCitas"; // Suponiendo que este es el nombre del procedimiento almacenado

            // Ejecutamos la consulta que devuelve una lista de citas
            var consulta = await _sqlConnection.QueryAsync<Cita>(sql, new { PersonaID = PersonaID }, commandType: CommandType.StoredProcedure);

            // Si no hay citas relacionadas, retornamos null
            if (!consulta.Any())
                return null;

            // Retornamos la lista de citas
            return consulta;
        }

        public async Task<Cita> ObtenerOne(Guid CitaID)
        {
            string sql = @"getOneCita";

            var consulta = await _sqlConnection.QueryAsync<Cita>(sql, new { CitaID = CitaID });

            if (!consulta.Any())
                return null;

            return consulta.First();
        }

        public async Task<IEnumerable<Cita>> ObtenerTodos()
        {
            string sql = @"getAllCitas";

            var consulta = await _sqlConnection.QueryAsync<Cita>(sql);

            if (!consulta.Any())
                return null;

            return consulta;
        }
    }
}
