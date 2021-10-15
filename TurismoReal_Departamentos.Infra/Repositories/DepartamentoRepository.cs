using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using TurismoReal_Departamentos.Core.DTOs;
using TurismoReal_Departamentos.Core.Interfaces;
using TurismoReal_Departamentos.Infra.Context;

namespace TurismoReal_Departamentos.Infra.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        protected readonly OracleContext _context;

        public DepartamentoRepository()
        {
            _context = new OracleContext();
        }

        // GET ALL
        public Task<List<object>> GetDepartamentos()
        {
            throw new NotImplementedException();
        }

        // GET BY ID
        public Task<object> GetDepartamento(int id)
        {
            throw new NotImplementedException();
        }

        // CREATE
        public async Task<int> CreateDepartamento(Departamento depto)
        {
            int saved = 0;
            try
            {
                _context.OpenConnection();
                OracleCommand cmd = new OracleCommand("sp_agregar_depto", _context.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.BindByName = true;
                cmd.Parameters.Add("rol_d", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("dormitorios_d", OracleDbType.Int32).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("banios_d", OracleDbType.Int32).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("descripcion_d", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("superficie_d", OracleDbType.Int32).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("valor_diario_d", OracleDbType.Double).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("tipo_d", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("comuna_d", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("calle_d", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("numero_d", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("depto_d", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("saved", OracleDbType.Int32).Direction = ParameterDirection.Output;

                cmd.Parameters["rol_d"].Value = depto.rol;
                cmd.Parameters["dormitorios_d"].Value = depto.dormitorios;
                cmd.Parameters["banios_d"].Value = depto.banios;
                cmd.Parameters["descripcion_d"].Value = depto.descripcion;
                cmd.Parameters["superficie_d"].Value = depto.superficie;
                cmd.Parameters["valor_diario_d"].Value = depto.valorDiario;
                cmd.Parameters["tipo_d"].Value = depto.tipo;
                cmd.Parameters["comuna_d"].Value = depto.direccion.comuna;
                cmd.Parameters["calle_d"].Value = depto.direccion.calle;
                cmd.Parameters["numero_d"].Value = depto.direccion.numero;
                cmd.Parameters["depto_d"].Value = depto.direccion.depto;

                await cmd.ExecuteNonQueryAsync();
                _context.CloseConnection();
                // Retorna el id del depto agregado
                saved = int.Parse(cmd.Parameters["saved"].Value.ToString());
                return saved;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return saved;
            }
        }

        // UPDATE
        public Task<object> UpdateDepartamento(int id, object depto)
        {
            throw new NotImplementedException();
        }

        // DELETE
        public Task<object> DeleteDepartamento(int id)
        {
            throw new NotImplementedException();
        }
    }
}
