namespace Gvn.GvnFramework.BackgroundJobs.Abstractions;

public interface IRecurringJob
{
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
