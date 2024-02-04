using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickDefendDB;


string localDbPath = @"C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVERDB\MSSQL\Backup";
string connectionStr = "Data Source=DESKTOP-HGKR5P9\\MSSQLSERVERDB;Initial Catalog=SkillHavenDB;Integrated Security=True;Encrypt=false;";

var hostBuilder = Host.CreateDefaultBuilder(args)
           .ConfigureServices((hostContext, services) =>
           {
               services.AddHostedService<ScheduledDBBackup>(serviceProvider =>
                   new ScheduledDBBackup(connectionStr,localDbPath,"* * * * *","backup",false));
           });

await hostBuilder.RunConsoleAsync();