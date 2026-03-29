using System.Linq.Expressions;

namespace Gvn.GvnFramework.BackgroundJobs.Abstractions;

public interface IBackgroundJobService
{
    string Enqueue(Expression<Action> job);
    string Enqueue<T>(Expression<Action<T>> job);
    string Schedule(Expression<Action> job, TimeSpan delay);
    string Schedule<T>(Expression<Action<T>> job, TimeSpan delay);
    void AddOrUpdateRecurring(string jobId, Expression<Action> job, string cronExpression);
    void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> job, string cronExpression);
    void Delete(string jobId);
}
