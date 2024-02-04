using Cronos;
using Microsoft.Extensions.Hosting;
using QuickDefendDB.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuickDefendDB.ConstantExpressions;

namespace QuickDefendDB;
public class ScheduledDBBackup : BackgroundService
{

    private readonly string connectionString;
    private readonly string databasePath;
    private readonly string? backupFileName = "Backup";
    private readonly string cronExpression = Daily;
    private readonly bool? isOverwriteFile = false;


    /// <summary>
    /// Get Scheduler Backup using this constructor
    /// </summary>
    /// <param name="connectionString">Connection string of database</param>
    /// <param name="databasePath">Database Folder Path.Make sure the path is accesible from your project or machine where library used </param>
    /// <param name="cronExpression">Defining the schedule on backup executed </param>
    /// <param name="backupFileName">Backup file name</param>
    /// <param name="isOverwriteFile">if true creates new file else overwrites existing file</param>
    /// <example>
    /// For adding web api enough to add:
    /// <code>
    /// builder.Services.AddHostedService(_ =>
    /// new ScheduledDBBackup(builder.Configuration.GetConnectionString("DefaultConnection"), builder.Configuration["LocalDbPath"]);
    /// </code>
    /// </example>
    public ScheduledDBBackup(string connectionString, string databasePath, string? cronExpression, string? backupFileName, bool? isOverwriteFile)
    {
        this.connectionString = connectionString;
        this.databasePath = databasePath;
        this.backupFileName = backupFileName;
        this.cronExpression = cronExpression;
        this.isOverwriteFile = isOverwriteFile;
    }


    /// <summary>
    /// Get Scheduler Backup using this constructor
    /// </summary>
    /// <param name="connectionString">Connection string of database</param>
    /// <param name="databasePath">Database Folder Path.Make sure the path is accesible from your project or machine where library used </param>
    /// <param name="isOverwriteFile">if true creates new file else overwrites existing file</param>
    public ScheduledDBBackup(string connectionString, string databasePath,bool isOverwriteFile=false)
    {
        this.connectionString = connectionString;
        this.databasePath = databasePath;
        this.isOverwriteFile = isOverwriteFile;
    }


    /// <summary>
    /// Get Scheduler Backup using this constructor
    /// </summary>
    /// <param name="connectionString">Connection string of database</param>
    /// <param name="databasePath">Database Folder Path.Make sure the path is accesible from your project or machine where library used </param>
    /// <param name="cronExpression">Defining the schedule on backup executed </param>
    /// <param name="isOverwriteFile">if true creates new file else overwrites existing file</param>
    public ScheduledDBBackup(string connectionString, string databasePath, string cronExpression,bool? isOverwriteFile=false)
    {
        this.connectionString = connectionString;
        this.databasePath = databasePath;
        this.cronExpression = cronExpression;
        this.isOverwriteFile = isOverwriteFile;
    }

 


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                BackupDB.Backup(connectionString, databasePath, backupFileName,isOverwriteFile);
                await WaitForNextSchedule(cronExpression);
            }
        }
        catch (Exception e)
        {
            throw new QuickDefendDBException(e.Message, e);
        }
    }

    private async Task WaitForNextSchedule(string cronExpression)
    {
        var parsedExp = CronExpression.Parse(cronExpression);
        var currentUtcTime = DateTimeOffset.UtcNow.UtcDateTime;
        var occurenceTime = parsedExp.GetNextOccurrence(currentUtcTime);

        var delay = occurenceTime.GetValueOrDefault() - currentUtcTime;

        await Task.Delay(delay);
    }

}
