using Hangfire.Dashboard;
using Hangfire.Dashboard.BasicAuthorization;

namespace Gvn.GvnFramework.BackgroundJobs.Authorization;

/// <summary>
/// Basic Auth filter for Hangfire Dashboard.
/// </summary>
internal sealed class HangfireDashboardAuthorizationFilter : BasicAuthAuthorizationFilter
{
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
