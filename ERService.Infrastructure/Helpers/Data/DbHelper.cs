using ERService.Infrastructure.Base.Common;
using MartinCostello.SqlLocalDb;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;

namespace ERService.Infrastructure.Helpers.Data
{
    public interface IDatabaseProvider
    {
        DatabaseProvider DatabaseProvider { get; }
        string ConnectionString { get; set; }
        bool IsReady();
    }

    public class MsSqlDataProvider : IDatabaseProvider
    {
        public DatabaseProvider DatabaseProvider => DatabaseProvider.MSSQLServer;

        public string ConnectionString { get; set; }

        public bool IsReady() {
            using (var connection = new SqlConnection(ConnectionString)) {
                try {
                    connection.Open();
                    return true;
                }
                catch {
                    return false;
                }
            }
        }
    }

    public class DatabaseProviders : Collection<IDatabaseProvider>
    {
        public bool CheckConnectionFor(DatabaseProvider databaseProvider) => 
            this.Single(x => x.DatabaseProvider == databaseProvider).IsReady();
    }

    public static class DbHelper
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static bool ServerHeartBeat(string connectionString, DatabaseProvider databaseProvider)
        {
            if (databaseProvider == DatabaseProvider.MSSQLServer)
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        _logger.Error(ex);
                        return false;
                    }
                }
            }
            else if (databaseProvider == DatabaseProvider.MySQLServer)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                        return false;
                    }
                }
            }
            else if (databaseProvider == DatabaseProvider.MSSQLServerLocalDb)
            {
                using (var localDbApi = new SqlLocalDbApi()) {
                    return localDbApi.IsLocalDBInstalled();
                }
            }

            return false;
        }
    }
}
