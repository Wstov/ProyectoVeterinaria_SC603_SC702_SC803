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
    public class ExpedienteDA : IExpedienteDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public ExpedienteDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> Agregar(Expediente expediente)
        {
            string sql = @"addExpediente";
            await _sqlConnection.ExecuteAsync(sql, new
            {
                MascotaID = expediente.MascotaID,
                Diagnostico = expediente.Diagnostico
            }, commandType: CommandType.StoredProcedure);
            return expediente.ExpedienteID;
        }

        public async Task<Guid> Editar(Expediente expediente)
        {
            string sql = @"editExpediente";
            await _sqlConnection.ExecuteAsync(sql, new
            {
                ExpedienteID = expediente.ExpedienteID,
                MascotaID = expediente.MascotaID,
                Diagnostico = expediente.Diagnostico
            }, commandType: CommandType.StoredProcedure);
            return expediente.ExpedienteID;
        }

        public async Task<Guid> Eliminar(Guid ExpedienteID)
        {
            string sql = @"deleteExpediente";
            await _sqlConnection.ExecuteAsync(sql, new { ExpedienteID = ExpedienteID }, commandType: CommandType.StoredProcedure);
            return ExpedienteID;
        }

        public async Task<IEnumerable<Expediente>> ObtenerPorMascota(Guid MascotaID)
        {
            string sql = @"getExpedientesPorMascota"; // Suponiendo que existe un SP para obtener expedientes por MascotaID
            var consulta = await _sqlConnection.QueryAsync<Expediente>(sql, new { MascotaID = MascotaID }, commandType: CommandType.StoredProcedure);
            return consulta.Any() ? consulta : null;
        }

        public async Task<Expediente> ObtenerOne(Guid ExpedienteID)
        {
            string sql = @"getExpediente";
            var consulta = await _sqlConnection.QueryAsync<Expediente>(sql, new { ExpedienteID = ExpedienteID }, commandType: CommandType.StoredProcedure);
            return consulta.FirstOrDefault();
        }

        public async Task<IEnumerable<Expediente>> ObtenerTodos()
        {
            string sql = @"getExpedientes";
            var consulta = await _sqlConnection.QueryAsync<Expediente>(sql, commandType: CommandType.StoredProcedure);
            return consulta.Any() ? consulta : null;
        }
    }
}
