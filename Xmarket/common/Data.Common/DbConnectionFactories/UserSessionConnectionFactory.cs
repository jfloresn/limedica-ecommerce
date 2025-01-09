

namespace Data.Common.DbConnectionFactories
{
    using System;
    using System.Configuration;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;

    using Infraestructure.Common.UserDatabaseConnection;

    public class UserSessionConnectionFactory : IDbConnectionFactory
    {
        private const string InvariantName = "System.Data.SqlClient";
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(InvariantName);
            if (providerFactory == null)
                throw new InvalidOperationException(String.Format("The '{0}' provider is not registered on the local machine.", InvariantName));

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = UserSessionConnectionFactory.GetSeguridadDB() ? UserSessionConnectionString : DefaultConnectionFactory.DefaultConnectionString;
            return connection;

        }

        public static string UserSessionConnectionString
        {
            get
            {
                if (UserSessionConnectionFactory.GetSeguridadDB())
                {
                    Connection connectionHeader = Connection.GetHeaderFromMessage();

                    var builder = new SqlConnectionStringBuilder(DefaultConnectionFactory.DefaultConnectionString)
                    {
                        UserID = connectionHeader.User,
                        Password = connectionHeader.Password
                    };
                    return builder.ConnectionString;
                }
                else 
                {
                    return DefaultConnectionFactory.DefaultConnectionString;
                }

                
            }
        }

        /// <summary>
        /// Requerimiento: Salida Alamacen
        /// Autor: Xtreme-JHFLORES
        /// Fecha: 07/09/2016
        /// Descripción: Adecuación para conexion a otra base de datos
        /// </summary>
        public static string TerminalSessionConnectionString
        {
            get
            {
                if (UserSessionConnectionFactory.GetSeguridadDB())
                {
                    Connection connectionHeader = Connection.GetHeaderFromMessage();

                    var builder = new SqlConnectionStringBuilder(DefaultConnectionFactory.TerminalConnectionString)
                    {
                        UserID = connectionHeader.User,
                        Password = connectionHeader.Password
                    };
                    return builder.ConnectionString;
                }
                else
                {
                    return DefaultConnectionFactory.TerminalConnectionString;
                }


            }
        }

        public static bool GetSeguridadDB()
        {
            var segdb = Convert.ToString(ConfigurationManager.AppSettings["SeguridadDB"]);
            return string.IsNullOrEmpty(segdb) ? false : Convert.ToBoolean(segdb);
        }


    }


}
