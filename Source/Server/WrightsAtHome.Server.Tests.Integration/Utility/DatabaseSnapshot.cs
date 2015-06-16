using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WrightsAtHome.Server.Tests.Integration.Utility
{
    public static class DatabaseSnapshot
    {
        private const string SpCreateSnapShotName = "SnapshotBackup_Create";
        private const string SpCreateSnapShot =
        @"CREATE PROCEDURE [dbo].[" + SpCreateSnapShotName + @"]
            @databaseName        varchar(512),
            @databaseLogicalName varchar(512),
            @snapshotBackupPath  varchar(512),
            @snapshotBackupName  varchar(512)
        AS
        BEGIN
            SET NOCOUNT ON;

            DECLARE @sql varchar(500)
            SELECT @sql = 'CREATE DATABASE ' + @snapshotBackupName +
                          ' ON (NAME=[' + @databaseLogicalName +
                          '], FILENAME=''' + @snapshotBackupPath + @snapshotBackupName + 
                          ''') AS SNAPSHOT OF [' + @databaseName + ']'
            EXEC(@sql)
        END";

        private const string SpRestoreSnapShotName = "SnapshotBackup_Restore";
        private const string SpRestoreSnapShot =
        @"CREATE PROCEDURE [dbo].[" + SpRestoreSnapShotName + @"]    
            @databaseName varchar(512),    
            @snapshotBackupName varchar(512)    
        AS    
        BEGIN    
            SET NOCOUNT ON;

            DECLARE @sql varchar(500)
            SET @sql  = 'ALTER DATABASE [' + @databaseName + '] SET SINGLE_USER WITH ROLLBACK IMMEDIATE'
            EXEC (@sql)

            RESTORE DATABASE @databaseName
            FROM DATABASE_SNAPSHOT = @snapshotBackupName

            SET @sql = 'ALTER DATABASE [' + @databaseName + '] SET MULTI_USER'
            EXEC (@sql)
        END";

        private const string SpDeleteSnapShotName = "SnapshotBackup_Delete";
        private const string SpDeleteSnapShot =
        @"CREATE PROCEDURE [dbo].[" + SpDeleteSnapShotName + @"]
            @snapshotBackupName varchar(512)
        AS
        BEGIN
            SET NOCOUNT ON;

            DECLARE @sql varchar(500)

            SELECT @sql = 'DROP DATABASE ' + @snapshotBackupName 
            EXEC(@sql) 
        END";

        private static string _masterDbConnectionString;
        private static string _dbName;
        private static ConnectionStringSettings _dbConnectionStringSettings;

        private static ConnectionStringSettings DbConnectionStringSettings
        {
            get
            {
                if (_dbConnectionStringSettings == null)
                    _dbConnectionStringSettings = ConfigurationManager.ConnectionStrings["SnapshotBackup"];

                return _dbConnectionStringSettings;
            }
        }

        /// <summary>
        /// Stored procedures should be executed against master database
        /// </summary>
        private static string MasterDbConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_masterDbConnectionString))
                {
                    var sqlConnection = new SqlConnection(DbConnectionStringSettings.ConnectionString);
                    _masterDbConnectionString = DbConnectionStringSettings.ConnectionString.Replace(sqlConnection.Database, "master");
                }
                return _masterDbConnectionString;
            }
        }

        private static string DbName
        {
            get
            {
                if (string.IsNullOrEmpty(_dbName))
                    _dbName = new SqlConnection(DbConnectionStringSettings.ConnectionString).Database.TrimStart('[').TrimEnd(']');

                return _dbName;
            }
        }

        public static void SetupStoredProcedures()
        {
            using (var conn = new SqlConnection(MasterDbConnectionString))
            {
                conn.Open();

                // Drop the existing stored procedures
                SqlCommand cmd;
                const string dropProcSql = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'P', N'PC')) DROP PROCEDURE [dbo].[{0}]";
                foreach (var spName in new[] { SpCreateSnapShotName, SpDeleteSnapShotName, SpRestoreSnapShotName })
                {
                    cmd = new SqlCommand(string.Format(dropProcSql, spName), conn);
                    cmd.ExecuteNonQuery();
                }

                // Create the stored procedures anew
                foreach (var createProcSql in new[] { SpCreateSnapShot, SpDeleteSnapShot, SpRestoreSnapShot })
                {
                    cmd = new SqlCommand(createProcSql, conn);
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        public static void CreateSnapShot()
        {
            var databaseName = new SqlParameter { ParameterName = "@databaseName", SqlValue = SqlDbType.VarChar, Value = DbName };
            var databaseLogicalName = new SqlParameter { ParameterName = "@databaseLogicalName", SqlValue = SqlDbType.VarChar, Value = ConfigurationManager.AppSettings["DatabaseLogicalName"] };
            var snapshotBackupPath = new SqlParameter { ParameterName = "@snapshotBackupPath", SqlValue = SqlDbType.VarChar, Value = ConfigurationManager.AppSettings["SnapshotBackupPath"] };
            var snapshotBackupName = new SqlParameter { ParameterName = "@snapshotBackupName", SqlValue = SqlDbType.VarChar, Value = ConfigurationManager.AppSettings["SnapshotBackupName"] };

            ExecuteStoredProcAgainstMaster(SpCreateSnapShotName, new[] { databaseName, databaseLogicalName, snapshotBackupPath, snapshotBackupName });
        }

        public static void DeleteSnapShot()
        {
            var snapshotBackupName = new SqlParameter { ParameterName = "@snapshotBackupName", SqlValue = SqlDbType.VarChar, Value = ConfigurationManager.AppSettings["SnapshotBackupName"] };

            ExecuteStoredProcAgainstMaster(SpDeleteSnapShotName, new[] { snapshotBackupName });
        }

        public static void RestoreSnapShot()
        {
            var databaseName = new SqlParameter { ParameterName = "@databaseName", SqlValue = SqlDbType.VarChar, Value = DbName };
            var snapshotBackupName = new SqlParameter { ParameterName = "@snapshotBackupName", SqlValue = SqlDbType.VarChar, Value = ConfigurationManager.AppSettings["SnapshotBackupName"] };

            ExecuteStoredProcAgainstMaster(SpRestoreSnapShotName, new[] { databaseName, snapshotBackupName });
        }

        private static void ExecuteStoredProcAgainstMaster(string storedProc, SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(MasterDbConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(storedProc, conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}