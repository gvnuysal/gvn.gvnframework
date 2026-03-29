using Gvn.GvnFramework.AspNetCore.Extensions;
using Gvn.GvnFramework.BackgroundJobs.DependencyInjection;
using Gvn.GvnFramework.Caching.DependencyInjection;
using Gvn.GvnFramework.Security.DependencyInjection;
using Gvn.GvnFramework.Swagger.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers ───────────────────────────────────────────────────────────────
builder.Services.AddControllers();

// ── Security (JWT) ────────────────────────────────────────────────────────────
builder.Services.AddGvnSecurity(jwt =>
{
    jwt.Secret        = builder.Configuration["Jwt:Secret"]!;
    jwt.Issuer        = builder.Configuration["Jwt:Issuer"]!;
    jwt.Audience      = builder.Configuration["Jwt:Audience"]!;
    jwt.ExpiryMinutes = int.Parse(builder.Configuration["Jwt:ExpiryMinutes"] ?? "60");
});

// ── Caching (Memory — Redis için UseRedis: true yapın) ────────────────────────
builder.Services.AddGvnCaching(cache =>
{
    cache.UseRedis             = bool.Parse(builder.Configuration["Cache:UseRedis"] ?? "false");
    cache.DefaultExpiryMinutes = int.Parse(builder.Configuration["Cache:DefaultExpiryMinutes"] ?? "30");
    cache.Prefix               = builder.Configuration["Cache:Prefix"] ?? "gvn:";
});

// ── Background Jobs (Hangfire In-Memory) ─────────────────────────────────────
builder.Services.AddGvnBackgroundJobs(hf =>
{
    hf.UseInMemory     = true;
    hf.WorkerCount     = 3;
    hf.EnableDashboard = true;
});

// ── Scalar / OpenAPI ──────────────────────────────────────────────────────────
builder.Services.AddGvnSwagger("Gvn.GvnFramework Sample API", "v1");

var app = builder.Build();

// ── Middleware ────────────────────────────────────────────────────────────────
app.UseGvnCorrelationId();
app.UseGvnExceptionHandling();

app.UseAuthentication();
app.UseAuthorization();

// ── Hangfire Dashboard → /hangfire ────────────────────────────────────────────
app.UseGvnHangfireDashboard("/hangfire");

// ── Scalar UI → /scalar/v1  |  OpenAPI JSON → /openapi/v1.json ───────────────
app.UseGvnSwagger();

app.MapControllers();

app.Run();
