# QuickDefendDB

QuickDefendDB is a library developed to easily schedule automated backups for databases.

## Features

- Schedule database backups with simple configuration.
- Support for custom backup file naming and overwrite policies.
- Flexible scheduling using cron expressions.

## Getting Started

### Configuration in Program.cs :
```
builder.Services.AddHostedService(_ =>
   new ScheduledDBBackup(builder.Configuration.GetConnectionString("DefaultConnection"),
                         builder.Configuration["LocalDbPath"],
                         ConstantExpressions.Minutely,
                         "backupFileName",
                         true));
```
 
> :warning: **Warning**: Ensure that the specified `databasePath` is accessible from your project or the machine where the library is used. The library requires direct access to this path to perform backups successfully. Failure to provide an accessible path may result in backup failures or errors.
