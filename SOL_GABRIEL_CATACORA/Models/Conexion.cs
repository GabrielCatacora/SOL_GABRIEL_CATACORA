using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Net;
using System.Xml.Linq;

namespace SOL_GABRIEL_CATACORA.Models
{
    public class Conexion
    {
        private static Conexion Con = null;
        protected string strOracle = string.Empty;
        public Conexion()
        {
        }

        public OracleConnection CrearConexion()
        {

            OracleConnection Cadena = new OracleConnection();
            try
            {
                Cadena.ConnectionString = "DATA SOURCE=localhost:1521/ORCL;TNS_ADMIN=C:\\Users\\ADMINISTRATOR\\Oracle\\network\\admin;PERSIST SECURITY INFO=True;USER ID=MKLVR; PASSWORD=Xvitox2013$;";
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }

            return Cadena;
        }

        public static Conexion getInstancia()
        {

            if (Con == null)
            {
                Con = new Conexion();
            }
            return Con;
        }

    }

}
