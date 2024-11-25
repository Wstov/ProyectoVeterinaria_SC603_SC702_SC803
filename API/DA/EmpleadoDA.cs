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
    public class EmpleadoDA : IEmpleadoDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public EmpleadoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> Agregar(Empleado empleado)
        {
            string sql = @"addEmpleado";
            await _sqlConnection.QueryAsync<Empleado>(sql, new
            {
                UsuarioID = empleado.UsuarioID,
                Especialidad = empleado.Especialidad
            }, commandType: CommandType.StoredProcedure);

            return empleado.EmpleadoID;
        }

        public async Task<Guid> Editar(Empleado empleado)
        {
            string sql = @"editEmpleado";
            await _sqlConnection.QueryAsync<Empleado>(sql, new
            {
                EmpleadoID = empleado.EmpleadoID,
                UsuarioID = empleado.UsuarioID,
                Especialidad = empleado.Especialidad
            }, commandType: CommandType.StoredProcedure);

            return empleado.EmpleadoID;
        }

        public async Task<Guid> Eliminar(Guid EmpleadoID)
        {
            string sql = @"deleteEmpleado";
            await _sqlConnection.QueryAsync<Empleado>(sql, new { EmpleadoID }, commandType: CommandType.StoredProcedure);

            return EmpleadoID;
        }

        public async Task<EmpleadoGet> ObtenerOne(Guid EmpleadoID)
        {
            string sql = @"getOneEmpleado";
            var consulta = await _sqlConnection.QueryAsync<EmpleadoGet>(sql, new { EmpleadoID }, commandType: CommandType.StoredProcedure);

            return consulta.FirstOrDefault();
        }

        public async Task<IEnumerable<EmpleadoGet>> ObtenerTodos()
        {
            string sql = @"getAllEmpleados";
            var consulta = await _sqlConnection.QueryAsync<EmpleadoGet>(sql, commandType: CommandType.StoredProcedure);

            return consulta.Any() ? consulta : null;
        }
    }
}
