using System.Linq.Expressions;
using Gvn.GvnFramework.BackgroundJobs.Abstractions;
using Hangfire;
using Hangfire.Tags;
using MediatR;

namespace Gvn.GvnFramework.BackgroundJobs.Implementations;

public sealed class HangfireJobService : IBackgroundJobService
{
    // Fire-and-forget
    public string Enqueue(Expression<Action> job)
        => BackgroundJob.Enqueue(job);

    public string Enqueue<T>(Expression<Action<T>> job)
        => BackgroundJob.Enqueue(job);

    public Task<string> EnqueueAsync<T>(Expression<Func<T, Task>> job)
        => Task.FromResult(BackgroundJob.Enqueue(job));

    // Tagged jobs
    public string EnqueueWithTag(Expression<Action> job, params string[] tags)
    {
        var jobId = BackgroundJob.Enqueue(job);
        jobId.AddTags(tags);
        return jobId;
    }

    public string EnqueueWithTag<T>(Expression<Action<T>> job, params string[] tags)
    {
        var jobId = BackgroundJob.Enqueue(job);
        jobId.AddTags(tags);
        return jobId;
    }

    // MediatR integration
    public string EnqueueMediator<TRequest>(TRequest request) where TRequest : IRequest
        => BackgroundJob.Enqueue<IMediator>(m => m.Send(request, CancellationToken.None));

    // Scheduled
    public string Schedule(Expression<Action> job, TimeSpan delay)
        => BackgroundJob.Schedule(job, delay);

    public string Schedule<T>(Expression<Action<T>> job, TimeSpan delay)
        => BackgroundJob.Schedule(job, delay);

    // Recurring
    public void AddOrUpdateRecurring(string jobId, Expression<Action> job, string cronExpression)
        => RecurringJob.AddOrUpdate(jobId, job, cronExpression);

    public void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> job, string cronExpression)
        => RecurringJob.AddOrUpdate(jobId, job, cronExpression);

    // Management
    public void Delete(string jobId)
        => BackgroundJob.Delete(jobId);

    public void Requeue(string jobId)
        => BackgroundJob.Requeue(jobId);
}
