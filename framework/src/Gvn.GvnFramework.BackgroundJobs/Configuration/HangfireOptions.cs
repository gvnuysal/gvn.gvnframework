namespace Gvn.GvnFramework.BackgroundJobs.Configuration;

public sealed class HangfireOptions
{
    public const string SectionName = "Hangfire";

    public bool UseInMemory { get; set; } = true;
    public string? SqlServerConnectionString { get; set; }
    public bool EnableDashboard { get; set; } = true;
    public string DashboardPath { get; set; } = "/hangfire";
    public int WorkerCount { get; set; } = 5;

    // Dashboard authorization
    public string? DashboardUsername { get; set; }
    public string? DashboardPassword { get; set; }

    // Extensions
    public bool UseConsole { get; set; } = true;
    public bool UseTags { get; set; } = true;
    public bool UseHeartbeat { get; set; } = true;
    public bool UseRecurringJobAdmin { get; set; } = true;
}
