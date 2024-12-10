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
    public class FacturaDA : IFacturaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public FacturaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorioDapper();
        }

        public async Task<Guid> Agregar(Factura factura)
        {
            string sql = @"addFactura";
            await _sqlConnection.QueryAsync(sql, new
            {
                factura.FacturaID,
                factura.IdPersona,
                factura.IdCarrito,
                factura.MetodoPago,
                factura.Total,
                factura.Comentarios
            }, commandType: CommandType.StoredProcedure);

            return factura.FacturaID;
        }

        public async Task<IEnumerable<Factura>> ObtenerTodos()
        {
            string sql = @"selectFacturas";
            var consulta = await _sqlConnection.QueryAsync<Factura>(sql, commandType: CommandType.StoredProcedure);

            return consulta.Any() ? consulta : null;
        }

        public async Task<Factura> ObtenerOne(Guid FacturaID)
        {
            string sql = @"selectFacturaID";
            var consulta = await _sqlConnection.QueryAsync<Factura>(sql, new { FacturaID }, commandType: CommandType.StoredProcedure);

            return consulta.FirstOrDefault();
        }

        public async Task<IEnumerable<Factura>> ObtenerPorCliente(Guid IdPersona)
        {
            string sql = @"selectFacturasCliente";
            var consulta = await _sqlConnection.QueryAsync<Factura>(sql, new { IdPersona }, commandType: CommandType.StoredProcedure);

            return consulta.Any() ? consulta : null;
        }
    }
}
