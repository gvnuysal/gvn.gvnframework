using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;

namespace Gvn.GvnFramework.BackgroundJobs.Authorization;

/// <summary>
/// Hangfire dashboard authorization filter that enforces HTTP Basic authentication
/// using the configured username and password from <see cref="Configuration.HangfireOptions"/>.
/// </summary>
internal sealed class HangfireDashboardAuthorizationFilter : BasicAuthAuthorizationFilter
{
    /// <summary>
    /// Initializes a new instance of <see cref="HangfireDashboardAuthorizationFilter"/>
    /// with the specified credentials.
    /// </summary>
    /// <param name="username">The required dashboard username.</param>
    /// <param name="password">The required dashboard password (clear-text).</param>
    public HangfireDashboardAuthorizationFilter(string username, string password)
        : base(new BasicAuthAuthorizationFilterOptions
        {
            RequireSsl = false,
            SslRedirect = false,
            LoginCaseSensitive = true,
            Users =
            [
                new BasicAuthAuthorizationUser
                {
                    Login = username,
                    PasswordClear = password
                }
            ]
        })
    {
    }
}
