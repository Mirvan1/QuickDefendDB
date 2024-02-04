using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickDefendDB;

/// <summary>
/// Constant Cron Expressions
/// </summary>
public static class ConstantExpressions
{
    /// <summary>
    /// Minutely Cron Expression
    /// </summary>
    public static string Minutely { get; } ="* * * * *";

    /// <summary>
    /// Daily Cron Expression
    /// </summary>
    public static string Daily { get; } = "0 0 * * *";

    /// <summary>
    /// Weekly Cron Expression
    /// </summary>
    public static string Weekly { get; } = "0 0 * * 0";

    /// <summary>
    /// Monthly Cron Expression
    /// </summary>
    public static string Monthly { get; } = "0 0 1 * *";

    /// <summary>
    /// Yearly Cron Expression
    /// </summary>
    public static string Yearly { get; } = "0 0 1 1 *";

}
