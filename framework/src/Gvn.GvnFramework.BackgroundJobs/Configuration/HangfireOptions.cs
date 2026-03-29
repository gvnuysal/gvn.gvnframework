namespace Gvn.GvnFramework.BackgroundJobs.Configuration;

public sealed class HangfireOptions
{
    public const string SectionName = "Hangfire";

    public bool UseInMemory { get; set; } = true;
    public string? SqlServerConnectionString { get; set; }
    public bool EnableDashboard { get; set; } = true;
    public string DashboardPath { get; set; } = "/hangfire";
    public int WorkerCount { get; set; } = 5;
}
