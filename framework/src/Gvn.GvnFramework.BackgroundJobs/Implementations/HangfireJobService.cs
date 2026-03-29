using System.Linq.Expressions;
using Gvn.GvnFramework.BackgroundJobs.Abstractions;
using Hangfire;

namespace Gvn.GvnFramework.BackgroundJobs.Implementations;

public sealed class HangfireJobService : IBackgroundJobService
{
    public string Enqueue(Expression<Action> job)
        => BackgroundJob.Enqueue(job);

    public string Enqueue<T>(Expression<Action<T>> job)
        => BackgroundJob.Enqueue(job);

    public string Schedule(Expression<Action> job, TimeSpan delay)
        => BackgroundJob.Schedule(job, delay);

    public string Schedule<T>(Expression<Action<T>> job, TimeSpan delay)
        => BackgroundJob.Schedule(job, delay);

    public void AddOrUpdateRecurring(string jobId, Expression<Action> job, string cronExpression)
        => RecurringJob.AddOrUpdate(jobId, job, cronExpression);

    public void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> job, string cronExpression)
        => RecurringJob.AddOrUpdate(jobId, job, cronExpression);

    public void Delete(string jobId)
        => BackgroundJob.Delete(jobId);
}
