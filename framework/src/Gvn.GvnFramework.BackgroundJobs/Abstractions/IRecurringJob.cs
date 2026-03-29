namespace Gvn.GvnFramework.BackgroundJobs.Abstractions;

/// <summary>
/// Marker interface for recurring background jobs.
/// Implement this interface and register the job with <see cref="IBackgroundJobService.AddOrUpdateRecurring{T}"/>
/// to schedule periodic execution.
/// </summary>
public interface IRecurringJob
{
    /// <summary>
    /// Executes the recurring job logic.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token that signals job cancellation.</param>
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
