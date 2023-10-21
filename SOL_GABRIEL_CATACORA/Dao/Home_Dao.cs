using Oracle.ManagedDataAccess.Client;
using SOL_GABRIEL_CATACORA.Models;
using System.Configuration;
using System;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SOL_GABRIEL_CATACORA.Dao
{
    public class Home_Dao
    {
        OracleConnection con;
        private List<ALUMNO_ENT> NAlumno;
        private List<CURSO_ENT> NCurso;
        private List<SESSION_ENT> NSession;
        private List<MATRICULA_ENT> NMatricula;
        private string Cadena = "DATA SOURCE=localhost:1521/ORCL;TNS_ADMIN=C:\\Users\\ADMINISTRATOR\\Oracle\\network\\admin;PERSIST SECURITY INFO=True;USER ID=MKLVR; PASSWORD=Xvitox2013$;";

        public Home_Dao()
        {
            NAlumno = new List<ALUMNO_ENT>();
            NCurso = new List<CURSO_ENT>();
            NSession = new List<SESSION_ENT>();
            NMatricula = new List<MATRICULA_ENT>();
        }
        public List<ALUMNO_ENT> ListarAlumnosDAO() 
        {
            OracleDataReader Resultado;
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Commando = new OracleCommand("SELECT DNI, CODIGOALUMNO, NOMBRE, APELLIDO, CREDITO, ESTADO FROM ALUMNO", SqlCon);
                Commando.CommandType = System.Data.CommandType.Text;
                SqlCon.Open();
                Resultado = Commando.ExecuteReader();

                while (Resultado.Read())
                {
                    ALUMNO_ENT alu = new ALUMNO_ENT();
                    alu.DNI = Convert.ToString(Resultado["DNI"]);
                    alu.CODIGOALUMNO = Convert.ToString(Resultado["CODIGOALUMNO"]);
                    alu.NOMBRE = Convert.ToString(Resultado["NOMBRE"]);
                    alu.APELLIDO = Convert.ToString(Resultado["APELLIDO"]);
                    alu.CREDITO = Convert.ToInt32(Resultado["CREDITO"]);
                    alu.ESTADO = Convert.ToString(Resultado["ESTADO"]);
                    NAlumno.Add(alu);
                }

                SqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NAlumno;
        }

        public List<CURSO_ENT> ListarCursoDAO() 
        {
            OracleDataReader Resultado;
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Commando = new OracleCommand("SELECT ID, IDSESSION, DESCRIPCIONCURSO, CANTCREDITO FROM CURSO", SqlCon);
                Commando.CommandType = System.Data.CommandType.Text;
                SqlCon.Open();
                Resultado = Commando.ExecuteReader();

                while (Resultado.Read())
                {
                    CURSO_ENT cur = new CURSO_ENT();
                    cur.ID = Convert.ToString(Resultado["ID"]);
                    cur.IDSESSION = Convert.ToString(Resultado["IDSESSION"]);
                    cur.DESCRIPCIONCURSO = Convert.ToString(Resultado["DESCRIPCIONCURSO"]);
                    cur.CANTCREDITO = Convert.ToInt32(Resultado["CANTCREDITO"]);
                    NCurso.Add(cur);
                }

                SqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NCurso;
        }

        public List<SESSION_ENT> ListarSessionDAO() 
        {
            OracleDataReader Resultado;
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Commando = new OracleCommand("SELECT ID, NOMBRE, VATANTES, ESTADO FROM SECCION", SqlCon);
                Commando.CommandType = System.Data.CommandType.Text;
                SqlCon.Open();
                Resultado = Commando.ExecuteReader();

                while (Resultado.Read())
                {
                    SESSION_ENT cur = new SESSION_ENT();
                    cur.ID = Convert.ToString(Resultado["ID"]);
                    cur.NOMBRE = Convert.ToString(Resultado["NOMBRE"]);
                    cur.VATANTES = Convert.ToInt32(Resultado["VATANTES"]);
                    cur.ESTADO = Convert.ToString(Resultado["ESTADO"]);
                    NSession.Add(cur);
                }

                SqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NSession;
        }

        public string RegistarMatriculaDAO(MATRICULA_ENT matri) 
        {
            string respuesta = "";
            OracleDataReader Resultado;
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Commando = new OracleCommand("INSERT INTO MATRICULA (CODIGOALUMNO, IDCURSO, IDSESSION, TIPOMATRICULA, FECHAREGISTRO, FECHAANULACION) VALUES(@CODIGOALUMNO, @IDCURSO, @IDSESSION, @TIPOMATRICULA, @FECHAREGISTRO, @FECHAANULACION) ", SqlCon);
                Commando.Parameters.Add("@CODIGOALUMNO", matri.idAlumno);
                Commando.Parameters.Add("@IDCURSO", matri.idCurso);
                Commando.Parameters.Add("@IDSESSION", matri.idSession);
                Commando.Parameters.Add("@TIPOMATRICULA", matri.idTipoMatricula);
                Commando.Parameters.Add("@FECHAREGISTRO", DateTime.Now.ToString());
                Commando.Parameters.Add("@FECHAANULACION", DateTime.Now.ToString());
                Commando.CommandType = System.Data.CommandType.Text;
                SqlCon.Open();
                Resultado = Commando.ExecuteReader();

                while (Resultado.Read()) 
                {
                    respuesta = "Se registro correctamente la matricula.";
                }

            }
            catch (Exception ex)
            {
                respuesta = "Hubo un error al registrar la matricula.";
                throw ex;
            }

            return respuesta;
        }

        public string RegistarMatriculaDAO_SP(MATRICULA_ENT matri)
        {
            string? result = "";
            try
            {
                using (OracleConnection cn = new OracleConnection(Cadena))
                {
                    cn.Open();
                    using (OracleCommand command = new OracleCommand("SP_INSERT_MATRICULA", cn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.Add(new OracleParameter("P_CODIGOALUMNO", OracleDbType.Varchar2)).Value = matri.idAlumno;
                        command.Parameters.Add(new OracleParameter("P_IDCURSO", OracleDbType.Varchar2)).Value = matri.idCurso;
                        command.Parameters.Add(new OracleParameter("P_IDSESSION", OracleDbType.Varchar2)).Value = matri.idSession;
                        command.Parameters.Add(new OracleParameter("P_TIPOMATRICULA", OracleDbType.Varchar2)).Value = matri.idTipoMatricula;
                        command.Parameters.Add(new OracleParameter("P_FECHAREGISTRO", OracleDbType.Date)).Value = DateTime.Now;
                        command.Parameters.Add(new OracleParameter("P_FECHAANULACION", OracleDbType.Date)).Value = DateTime.Now;
                        command.Parameters.Add(new OracleParameter("P_CREDITO", OracleDbType.Int32)).Value = matri.idCredito;
                        command.Parameters.Add(new OracleParameter("P_RESULT", OracleDbType.Varchar2, 50)).Value = System.Data.ParameterDirection.Output;
                        command.ExecuteNonQuery();
                        result = Convert.ToString(command.Parameters["P_RESULT"].Value);
                    }
                    cn.Close();
                }
            }
            catch (Exception? ex)
            {
                throw ex;
            }

            return result!;
        }

        public List<MATRICULA_ENT> ListarMatriculaDAO() 
        {
            OracleDataReader Resultado;
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                OracleCommand Commando = new OracleCommand("SELECT CODIGOALUMNO, IDCURSO, IDSESSION, TIPOMATRICULA FROM MATRICULA", SqlCon);
                Commando.CommandType = System.Data.CommandType.Text;
                SqlCon.Open();
                Resultado = Commando.ExecuteReader();

                while (Resultado.Read())
                {
                    MATRICULA_ENT cur = new MATRICULA_ENT();
                    cur.idAlumno = Convert.ToString(Resultado["CODIGOALUMNO"]);
                    cur.idCurso = Convert.ToString(Resultado["IDCURSO"]);
                    cur.idSession = Convert.ToString(Resultado["IDSESSION"]);
                    cur.idTipoMatricula = Convert.ToString(Resultado["TIPOMATRICULA"]);
                    NMatricula.Add(cur);
                }

                SqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return NMatricula;
        }

    }
}
