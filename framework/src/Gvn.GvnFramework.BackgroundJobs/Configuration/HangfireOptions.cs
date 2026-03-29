namespace Gvn.GvnFramework.BackgroundJobs.Configuration;

/// <summary>
/// Configuration options for the Hangfire background job infrastructure.
/// Bind from the <c>"Hangfire"</c> section of <c>appsettings.json</c>.
/// </summary>
public sealed class HangfireOptions
{
    /// <summary>The configuration section key used to bind these options.</summary>
    public const string SectionName = "Hangfire";

    /// <summary>
    /// Gets or sets a value indicating whether to use in-memory storage.
    /// When <c>false</c>, <see cref="SqlServerConnectionString"/> must be provided. Defaults to <c>true</c>.
    /// </summary>
    public bool UseInMemory { get; set; } = true;

    /// <summary>Gets or sets the SQL Server connection string used when <see cref="UseInMemory"/> is <c>false</c>.</summary>
    public string? SqlServerConnectionString { get; set; }

    /// <summary>Gets or sets a value indicating whether the Hangfire dashboard is enabled. Defaults to <c>true</c>.</summary>
    public bool EnableDashboard { get; set; } = true;

    /// <summary>Gets or sets the URL path for the Hangfire dashboard. Defaults to <c>"/hangfire"</c>.</summary>
    public string DashboardPath { get; set; } = "/hangfire";

    /// <summary>Gets or sets the number of Hangfire worker threads. Defaults to <c>5</c>.</summary>
    public int WorkerCount { get; set; } = 5;

    /// <summary>Gets or sets the username for basic-auth protection of the dashboard. Leave <c>null</c> to disable.</summary>
    public string? DashboardUsername { get; set; }

    /// <summary>Gets or sets the password for basic-auth protection of the dashboard. Leave <c>null</c> to disable.</summary>
    public string? DashboardPassword { get; set; }

    /// <summary>Gets or sets a value indicating whether the Hangfire.Console extension is enabled. Defaults to <c>true</c>.</summary>
    public bool UseConsole { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether the FaceIT.Hangfire.Tags extension is enabled. Defaults to <c>true</c>.</summary>
    public bool UseTags { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether the Hangfire.Heartbeat extension is enabled. Defaults to <c>true</c>.</summary>
    public bool UseHeartbeat { get; set; } = true;

    /// <summary>Gets or sets a value indicating whether the Hangfire.RecurringJobAdmin extension is enabled. Defaults to <c>true</c>.</summary>
    public bool UseRecurringJobAdmin { get; set; } = true;
}
