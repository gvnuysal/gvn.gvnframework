using System.Linq.Expressions;
using MediatR;

namespace Gvn.GvnFramework.BackgroundJobs.Abstractions;

public interface IBackgroundJobService
{
    // Fire-and-forget
    string Enqueue(Expression<Action> job);
    string Enqueue<T>(Expression<Action<T>> job);
    Task<string> EnqueueAsync<T>(Expression<Func<T, Task>> job);

    // Tagged jobs
    string EnqueueWithTag(Expression<Action> job, params string[] tags);
    string EnqueueWithTag<T>(Expression<Action<T>> job, params string[] tags);

    // MediatR integration
    string EnqueueMediator<TRequest>(TRequest request) where TRequest : IRequest;

    // Scheduled
    string Schedule(Expression<Action> job, TimeSpan delay);
    string Schedule<T>(Expression<Action<T>> job, TimeSpan delay);

    // Recurring
    void AddOrUpdateRecurring(string jobId, Expression<Action> job, string cronExpression);
    void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> job, string cronExpression);

    // Management
    void Delete(string jobId);
    void Requeue(string jobId);
}
