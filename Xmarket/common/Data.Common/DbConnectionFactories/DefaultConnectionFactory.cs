

namespace Data.Common.DbConnectionFactories
{
    using Infraestructure.Common.UserDatabaseConnection;
    using System;
    using System.Configuration;
    using System.Data.Common;
    using System.Data.Entity.Infrastructure;

    public class DefaultConnectionFactory : IDbConnectionFactory
    {
        private const string InvariantName = "System.Data.SqlClient";
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(InvariantName);
            if (providerFactory == null)
                throw new InvalidOperationException(String.Format("The '{0}' provider is not registered on the local machine.", InvariantName));

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = DefaultConnectionString;
            return connection;

        }

        /// <summary>
        /// Requerimiento: Salida Alamacen
        /// Autor: Xtreme-JHFLORES
        /// Fecha: 07/09/2016
        /// Descripción: Adecuación para conexion a otra base de datos
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        /// <returns></returns>
        public DbConnection CreateConnectionTerminal(string nameOrConnectionString)
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(InvariantName);
            if (providerFactory == null)
                throw new InvalidOperationException(String.Format("The '{0}' provider is not registered on the local machine.", InvariantName));

            DbConnection connection = providerFactory.CreateConnection();
            connection.ConnectionString = TerminalConnectionString;
            return connection;

        }

        public static string DefaultConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CnnSeg"].ConnectionString;
            }
        }

        /// <summary>
        /// Requerimiento: Salida Alamacen
        /// Autor: Xtreme-JHFLORES
        /// Fecha: 07/09/2016
        /// Descripción: Adecuación para conexion a otra base de datos
        /// </summary>
        public static string TerminalConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Terminal"].ConnectionString;
            }
        }
            
    }

    
}
