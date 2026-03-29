using System.Linq.Expressions;
using MediatR;

namespace Gvn.GvnFramework.BackgroundJobs.Abstractions;

/// <summary>
/// Provides an abstraction over Hangfire for enqueuing, scheduling, and managing background jobs.
/// </summary>
public interface IBackgroundJobService
{
    // Fire-and-forget

    /// <summary>Enqueues a fire-and-forget job with no service dependency.</summary>
    /// <param name="job">The job expression to execute.</param>
    /// <returns>The Hangfire job ID.</returns>
    string Enqueue(Expression<Action> job);

    /// <summary>Enqueues a fire-and-forget job resolved from the DI container.</summary>
    /// <typeparam name="T">The service type to resolve.</typeparam>
    /// <param name="job">The job expression to execute against the resolved service.</param>
    /// <returns>The Hangfire job ID.</returns>
    string Enqueue<T>(Expression<Action<T>> job);

    /// <summary>Enqueues an asynchronous fire-and-forget job resolved from the DI container.</summary>
    /// <typeparam name="T">The service type to resolve.</typeparam>
    /// <param name="job">The async job expression to execute against the resolved service.</param>
    /// <returns>A task that resolves to the Hangfire job ID.</returns>
    Task<string> EnqueueAsync<T>(Expression<Func<T, Task>> job);

    // Tagged jobs

    /// <summary>Enqueues a fire-and-forget job and applies one or more tags to it.</summary>
    /// <param name="job">The job expression to execute.</param>
    /// <param name="tags">Tags to attach to the job for filtering in the dashboard.</param>
    /// <returns>The Hangfire job ID.</returns>
    string EnqueueWithTag(Expression<Action> job, params string[] tags);

    /// <summary>Enqueues a fire-and-forget job resolved from DI and applies one or more tags to it.</summary>
    /// <typeparam name="T">The service type to resolve.</typeparam>
    /// <param name="job">The job expression to execute against the resolved service.</param>
    /// <param name="tags">Tags to attach to the job.</param>
    /// <returns>The Hangfire job ID.</returns>
    string EnqueueWithTag<T>(Expression<Action<T>> job, params string[] tags);

    // MediatR integration

    /// <summary>
    /// Enqueues a MediatR <see cref="IRequest"/> to be dispatched in the background via <see cref="IMediator.Send"/>.
    /// </summary>
    /// <typeparam name="TRequest">A MediatR request type.</typeparam>
    /// <param name="request">The request instance to dispatch.</param>
    /// <returns>The Hangfire job ID.</returns>
    string EnqueueMediator<TRequest>(TRequest request) where TRequest : IRequest;

    // Scheduled

    /// <summary>Schedules a delayed job with no service dependency.</summary>
    /// <param name="job">The job expression to execute.</param>
    /// <param name="delay">The time to wait before the job is enqueued.</param>
    /// <returns>The Hangfire job ID.</returns>
    string Schedule(Expression<Action> job, TimeSpan delay);

    /// <summary>Schedules a delayed job resolved from the DI container.</summary>
    /// <typeparam name="T">The service type to resolve.</typeparam>
    /// <param name="job">The job expression to execute against the resolved service.</param>
    /// <param name="delay">The time to wait before the job is enqueued.</param>
    /// <returns>The Hangfire job ID.</returns>
    string Schedule<T>(Expression<Action<T>> job, TimeSpan delay);

    // Recurring

    /// <summary>Adds or updates a recurring job with no service dependency.</summary>
    /// <param name="jobId">The unique recurring job identifier.</param>
    /// <param name="job">The job expression to execute.</param>
    /// <param name="cronExpression">A CRON expression that controls the schedule.</param>
    void AddOrUpdateRecurring(string jobId, Expression<Action> job, string cronExpression);

    /// <summary>Adds or updates a recurring job resolved from the DI container.</summary>
    /// <typeparam name="T">The service type to resolve.</typeparam>
    /// <param name="jobId">The unique recurring job identifier.</param>
    /// <param name="job">The job expression to execute against the resolved service.</param>
    /// <param name="cronExpression">A CRON expression that controls the schedule.</param>
    void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> job, string cronExpression);

    // Management

    /// <summary>Deletes a job by its Hangfire job ID.</summary>
    /// <param name="jobId">The ID of the job to delete.</param>
    void Delete(string jobId);

    /// <summary>Requeues an existing job so it runs again immediately.</summary>
    /// <param name="jobId">The ID of the job to requeue.</param>
    void Requeue(string jobId);
}
