

using Data.Common.DbConnectionFactories;
using System;
using System.Configuration;
using System.Data.SqlClient;
namespace Data.Common
{
    public class ConnectionFactory
    {
        public static SqlConnection CreateDefault()
        {
            var connection = new SqlConnection(DefaultConnectionFactory.DefaultConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }
        }

        public static SqlConnection CreateFromSecuritySession()
        {
            var connection = new SqlConnection(DefaultConnectionFactory.DefaultConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }


        }
        public static SqlConnection CreateFromUserSession()
        {
            var connection = new SqlConnection(UserSessionConnectionFactory.UserSessionConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }

        }

        /// <summary>
        /// Requerimiento: Salida Alamacen
        /// Autor: Xtreme-JHFLORES
        /// Fecha: 07/09/2016
        /// Descripción: Adecuación para conexion a otra base de datos
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateFromTerminalSession()
        {
            var connection = new SqlConnection(UserSessionConnectionFactory.TerminalSessionConnectionString);
            try
            {
                connection.Open();
                return connection;
            }
            catch (Exception)
            {
                connection.Close();
                connection.Dispose();
                throw;
            }

        }
    }
}
