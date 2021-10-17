using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<List<Departamento>> GetDepartamentos()
        {
            _context.OpenConnection();
            OracleCommand cmd = new OracleCommand("sp_obten_deptos", _context.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.BindByName = true;
            cmd.Parameters.Add("deptos", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            OracleDataReader reader = (OracleDataReader) await cmd.ExecuteReaderAsync();

            List<Departamento> deptos = new List<Departamento>();
            while (reader.Read())
            {
                Departamento depto = new Departamento();
                depto.id_departamento = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("id_departamento")).ToString());
                depto.rol = reader.GetValue(reader.GetOrdinal("rol")).ToString();
                depto.dormitorios = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("dormitorio")).ToString());
                depto.banios = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("banios")).ToString());
                depto.descripcion = reader.GetValue(reader.GetOrdinal("descripcion")).ToString();
                depto.superficie = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("superficie")).ToString());
                depto.valorDiario = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("valor_diario")).ToString());
                depto.tipo = reader.GetValue(reader.GetOrdinal("tipo_departamento")).ToString();
                depto.estado = reader.GetValue(reader.GetOrdinal("estado")).ToString();

                Direccion direccion = new Direccion();
                direccion.region = reader.GetValue(reader.GetOrdinal("region")).ToString();
                direccion.comuna = reader.GetValue(reader.GetOrdinal("comuna")).ToString();
                direccion.calle = reader.GetValue(reader.GetOrdinal("calle")).ToString();
                direccion.numero =reader.GetValue(reader.GetOrdinal("numero")).ToString();
                direccion.depto = reader.GetValue(reader.GetOrdinal("depto")).ToString();
                depto.direccion = direccion;

                // obtener instalaciones
                depto.instalaciones = ObtenerInstalaciones(depto, _context.GetConnection());
                deptos.Add(depto);
            }
            _context.CloseConnection();
            return deptos;
        }

        // GET BY ID
        public async Task<Departamento> GetDepartamento(int id)
        {
            _context.OpenConnection();
            OracleCommand cmd = new OracleCommand("sp_obten_depto_por_id", _context.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.BindByName = true;
            cmd.Parameters.Add("depto_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("depto", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Parameters["depto_id"].Value = id;
            OracleDataReader reader = (OracleDataReader) await cmd.ExecuteReaderAsync();

            Departamento depto = new Departamento();
            while (reader.Read())
            {
                depto.id_departamento = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("id_departamento")).ToString());
                depto.rol = reader.GetValue(reader.GetOrdinal("rol")).ToString();
                depto.dormitorios = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("dormitorio")).ToString());
                depto.banios = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("banios")).ToString());
                depto.descripcion = reader.GetValue(reader.GetOrdinal("descripcion")).ToString();
                depto.superficie = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("superficie")).ToString());
                depto.valorDiario = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("valor_diario")).ToString());
                depto.tipo = reader.GetValue(reader.GetOrdinal("tipo_departamento")).ToString();
                depto.estado = reader.GetValue(reader.GetOrdinal("estado")).ToString();

                Direccion direccion = new Direccion();
                direccion.region = reader.GetValue(reader.GetOrdinal("region")).ToString();
                direccion.comuna = reader.GetValue(reader.GetOrdinal("comuna")).ToString();
                direccion.calle = reader.GetValue(reader.GetOrdinal("calle")).ToString();
                direccion.numero = reader.GetValue(reader.GetOrdinal("numero")).ToString();
                direccion.depto = reader.GetValue(reader.GetOrdinal("depto")).ToString();
                depto.instalaciones = ObtenerInstalaciones(depto, _context.GetConnection());
            }
            _context.CloseConnection();
            return depto;
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
                // Retorna el id del depto agregado
                saved = int.Parse(cmd.Parameters["saved"].Value.ToString());

                if(depto.instalaciones.Count > 0)
                {
                    // Agregar instalaciones
                    depto.id_departamento = saved;
                    AgregarInstalaciones(depto, _context.GetConnection());
                }
                _context.CloseConnection();
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
        public async Task<int> DeleteDepartamento(int id)
        {
            _context.OpenConnection();
            OracleCommand cmd = new OracleCommand("sp_eliminar_depto", _context.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.BindByName = true;

            cmd.Parameters.Add("depto_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("removed", OracleDbType.Int32).Direction = ParameterDirection.Output;

            cmd.Parameters["depto_id"].Value = id;
            await cmd.ExecuteNonQueryAsync();
            int removed = Convert.ToInt32(cmd.Parameters["removed"].Value.ToString());
            return removed;
        }


        // AGREGAR INSTALACIONES DEPTO
        public void AgregarInstalaciones(Departamento depto, OracleConnection con)
        {
            OracleCommand cmd = new OracleCommand("sp_agregar_instalaciones", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.BindByName = true;

            cmd.Parameters.Add("depto_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("instalacion_d", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("success_sp", OracleDbType.Int32).Direction = ParameterDirection.Output;

            cmd.Parameters["depto_id"].Value = depto.id_departamento;

            foreach (string instalacion in depto.instalaciones)
            {
                cmd.Parameters["instalacion_d"].Value = instalacion;
                cmd.ExecuteNonQuery();
            }
        }

        // OBTENER INSTALACIONES DEPTO
        public List<string> ObtenerInstalaciones(Departamento depto, OracleConnection con)
        {
            OracleCommand cmd = new OracleCommand("sp_obten_instalaciones", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.BindByName = true;
            cmd.Parameters.Add("depto_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("instalaciones", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Parameters["depto_id"].Value = depto.id_departamento;
            OracleDataReader reader = cmd.ExecuteReader();

            List<string> instalaciones = new List<string>();
            while (reader.Read())
            {
                string instalacion = reader.GetValue(reader.GetOrdinal("instalacion")).ToString();
                instalaciones.Add(instalacion);
            }
            return instalaciones;
        }

    }
}
