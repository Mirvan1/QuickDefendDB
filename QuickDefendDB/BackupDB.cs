using QuickDefendDB.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Design;

namespace QuickDefendDB;
internal class BackupDB
{
    internal static void Backup(string connectionString, string DatabasePath, string BackupFileName,bool? isOverwriteFile)
    {
        if (string.IsNullOrEmpty(DatabasePath)) throw new QuickDefendDBException($"{nameof(DatabasePath)} cannot be empty");

        if (string.IsNullOrEmpty(connectionString)) throw new QuickDefendDBException($"{nameof(connectionString)} cannot be empty");

        if (!Directory.Exists(DatabasePath)) throw new QuickDefendDBException($"{nameof(DatabasePath)} is not accessible or not found");

        if (!DatabasePath.EndsWith(".bak") || !Path.GetExtension(DatabasePath).Equals(".bak"))
        {
            if(isOverwriteFile!=null && isOverwriteFile==true)
                DatabasePath = Path.Combine(DatabasePath, BackupFileName +"_"+ DateTime.UtcNow.ToFileTime() + ".bak");
            else
                DatabasePath = Path.Combine(DatabasePath, BackupFileName + ".bak");
        }

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
        string databaseName = builder.InitialCatalog;

        string query = $@"
                        USE [{databaseName}]; 
                        BACKUP DATABASE [{databaseName}]
                        TO DISK = '{DatabasePath}'
                        WITH FORMAT, MEDIANAME = 'DatabaseToolkitBackup_{databaseName}', NAME = 'Full Backup of {databaseName}'";

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                    command.ExecuteNonQuery();

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            throw new QuickDefendDBException(ex.Message, ex);
        }
    }
}
