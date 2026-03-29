using System.Linq.Expressions;
using Gvn.GvnFramework.BackgroundJobs.Abstractions;
using Hangfire;
using Hangfire.Tags;
using MediatR;

namespace Gvn.GvnFramework.BackgroundJobs.Implementations;

/// <summary>
/// Hangfire-based implementation of <see cref="IBackgroundJobService"/>.
/// Delegates all job management to the Hangfire <see cref="BackgroundJob"/> and <see cref="RecurringJob"/> APIs.
/// </summary>
public sealed class HangfireJobService : IBackgroundJobService
{
    // Fire-and-forget
    /// <inheritdoc />
    public string Enqueue(Expression<Action> job)
        => BackgroundJob.Enqueue(job);

    /// <inheritdoc />
    public string Enqueue<T>(Expression<Action<T>> job)
        => BackgroundJob.Enqueue(job);

    /// <inheritdoc />
    public Task<string> EnqueueAsync<T>(Expression<Func<T, Task>> job)
        => Task.FromResult(BackgroundJob.Enqueue(job));

    // Tagged jobs
    /// <inheritdoc />
    public string EnqueueWithTag(Expression<Action> job, params string[] tags)
    {
        var jobId = BackgroundJob.Enqueue(job);
        jobId.AddTags(tags);
        return jobId;
    }

    /// <inheritdoc />
    public string EnqueueWithTag<T>(Expression<Action<T>> job, params string[] tags)
    {
        var jobId = BackgroundJob.Enqueue(job);
        jobId.AddTags(tags);
        return jobId;
    }

    // MediatR integration
    /// <inheritdoc />
    public string EnqueueMediator<TRequest>(TRequest request) where TRequest : IRequest
        => BackgroundJob.Enqueue<IMediator>(m => m.Send(request, CancellationToken.None));

    // Scheduled
    /// <inheritdoc />
    public string Schedule(Expression<Action> job, TimeSpan delay)
        => BackgroundJob.Schedule(job, delay);

    /// <inheritdoc />
    public string Schedule<T>(Expression<Action<T>> job, TimeSpan delay)
        => BackgroundJob.Schedule(job, delay);

    // Recurring
    /// <inheritdoc />
    public void AddOrUpdateRecurring(string jobId, Expression<Action> job, string cronExpression)
        => RecurringJob.AddOrUpdate(jobId, job, cronExpression);

    /// <inheritdoc />
    public void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> job, string cronExpression)
        => RecurringJob.AddOrUpdate(jobId, job, cronExpression);

    // Management
    /// <inheritdoc />
    public void Delete(string jobId)
        => BackgroundJob.Delete(jobId);

    /// <inheritdoc />
    public void Requeue(string jobId)
        => BackgroundJob.Requeue(jobId);
}
