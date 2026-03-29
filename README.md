<div align="center">

<img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dotnetcore/dotnetcore-original.svg" width="100" height="100" alt=".NET Core"/>

# 🏗️ Gvn.GvnFramework

**Production-ready .NET 10 Clean Architecture Framework**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com)
[![C#](https://img.shields.io/badge/C%23-14.0-239120?style=for-the-badge&logo=csharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)](LICENSE)
[![GitHub Stars](https://img.shields.io/github/stars/gvnuysal/gvn.gvnframework?style=for-the-badge&color=gold)](https://github.com/gvnuysal/gvn.gvnframework/stargazers)

[![Build](https://img.shields.io/badge/Build-Passing-brightgreen?style=flat-square&logo=github-actions)](https://github.com/gvnuysal/gvn.gvnframework)
[![MediatR](https://img.shields.io/badge/MediatR-12.x-blue?style=flat-square)](https://github.com/jbogard/MediatR)
[![FluentValidation](https://img.shields.io/badge/FluentValidation-11.x-orange?style=flat-square)](https://fluentvalidation.net)
[![EF Core](https://img.shields.io/badge/EF%20Core-9.x-purple?style=flat-square)](https://docs.microsoft.com/en-us/ef/core/)
[![Serilog](https://img.shields.io/badge/Serilog-9.x-pink?style=flat-square)](https://serilog.net)
[![Hangfire](https://img.shields.io/badge/Hangfire-1.8-red?style=flat-square)](https://www.hangfire.io)
[![Scalar](https://img.shields.io/badge/Scalar-2.x-teal?style=flat-square)](https://scalar.com)

<br/>

*Modüler, ölçeklenebilir ve best-practice odaklı .NET framework altyapısı*

</div>

---

## 📋 İçindekiler

- [Genel Bakış](#-genel-bakış)
- [Mimari](#-mimari)
- [Modüller](#-modüller)
- [Hızlı Başlangıç](#-hızlı-başlangıç)
- [Modül Detayları](#-modül-detayları)
- [Örnek Kullanım](#-örnek-kullanım)
- [Konfigürasyon](#-konfigürasyon)
- [Proje Yapısı](#-proje-yapısı)
- [Teknolojiler](#-teknolojiler)

---

## 🎯 Genel Bakış

**Gvn.GvnFramework**, .NET 10 üzerine inşa edilmiş, **Clean Architecture** prensiplerine dayanan modüler bir framework altyapısıdır. Her modül bağımsız olarak kullanılabilir ve birbirleriyle uyumlu çalışacak şekilde tasarlanmıştır.

### ✨ Öne Çıkan Özellikler

| Özellik | Açıklama |
|---------|----------|
| 🧱 **Clean Architecture** | Domain → Application → Infrastructure → Presentation katman ayrımı |
| ⚡ **CQRS + MediatR** | Command/Query ayrımı, pipeline behaviors |
| 🛡️ **JWT Security** | Token üretimi, doğrulama, BCrypt şifreleme |
| 📦 **Generic Repository** | Specification pattern, Unit of Work, Soft Delete |
| 📊 **Structured Logging** | Serilog ile correlation ID, enricher desteği |
| 🚀 **Redis Caching** | Cache-aside pattern, Memory fallback |
| ⏰ **Background Jobs** | Hangfire ile zamanlanmış iş yönetimi |
| 📄 **Scalar UI** | Modern API dokümantasyonu |
| 🔔 **Domain Events** | Outbox pattern ile güvenilir event yayını |
| 🧩 **Modularity** | Assembly-scan ile otomatik modül yükleme |

---

## 🏛️ Mimari

```
┌─────────────────────────────────────────────────────────────┐
│              🌐 Presentation Layer                          │
│         AspNetCore · Swagger(Scalar) · BackgroundJobs       │
└──────────────────────────┬──────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────────┐
│              ⚙️  Application Layer                          │
│        CQRS · MediatR · FluentValidation · Pipeline         │
└──────────────────────────┬──────────────────────────────────┘
                           │
┌──────────────────────────▼──────────────────────────────────┐
│              🏢 Domain Layer                                │
│     Entity · AggregateRoot · ValueObject · DomainEvents     │
└──────────────────────────┬──────────────────────────────────┘
                           │ (interfaces)
┌──────────────────────────▼──────────────────────────────────┐
│              🔧 Infrastructure Layer                        │
│     EF Core · Redis · Serilog · Hangfire · Security         │
└─────────────────────────────────────────────────────────────┘
                           ▲
┌──────────────────────────┴──────────────────────────────────┐
│              💎 Core Layer  (tüm katmanlar kullanır)        │
│          Guard · Result<T> · Exceptions · Extensions        │
└─────────────────────────────────────────────────────────────┘
```

---

## 📦 Modüller

<table>
<tr>
<td width="50%">

### 💎 Core
```
Guard          → Null/range/collection doğrulama
Result<T>      → Operation result pattern
Error          → Typed error (NotFound, Validation...)
ErrorType      → Enum (Failure/Validation/NotFound...)
Exceptions     → GvnException hiyerarşisi
Extensions     → String, Enumerable helpers
```

### 🏢 Domain
```
Entity           → Base entity (Guid Id)
AuditableEntity  → CreatedAt/UpdatedAt
AggregateRoot    → Domain event yönetimi
ValueObject      → Equality component pattern
IDomainEvent     → Event abstraction
IRepository      → CRUD interface
IUnitOfWork      → Transaction abstraction
ISoftDeletable   → Soft delete interface
```

### ⚙️ Application
```
ICommand/IQuery        → CQRS abstractions
ValidationBehavior     → FluentValidation pipeline
LoggingBehavior        → Request/Response logging
PerformanceBehavior    → Slow query detection
PagedRequest/Result    → Pagination support
```

</td>
<td width="50%">

### 🗄️ EntityFrameworkCore
```
GvnDbContext         → Audit + SoftDelete + Events
EfRepository         → IRepository implementation
EfReadRepository     → No-tracking queries
UnitOfWork           → SaveChanges wrapper
Specification        → Query specification pattern
OutboxMessage        → Reliable event delivery
```

### 🔐 Security
```
ICurrentUserService  → HttpContext user bilgisi
ITokenService        → JWT üretim/doğrulama
IPasswordHasher      → BCrypt hash/verify
JwtOptions           → Secret, Issuer, Audience
```

### 📊 Logging · 🚀 Caching
```
Logging:
  SerilogConfiguration   → Console + File sinks
  CorrelationIdEnricher  → Request korelasyon

Caching:
  ICacheService      → GetOrSet, Remove...
  RedisCacheService  → StackExchange.Redis
  MemoryCacheService → IMemoryCache fallback
```

### ⏰ BackgroundJobs · 📄 Swagger
```
BackgroundJobs:
  IBackgroundJobService  → Enqueue/Schedule/Recurring
  HangfireJobService     → Hangfire implementation

Swagger (Scalar):
  AddGvnSwagger()        → OpenAPI + Scalar UI
  JwtSecurityScheme      → Bearer token UI
```

</td>
</tr>
</table>

---

## 🚀 Hızlı Başlangıç

### Gereksinimler

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Redis *(Caching için, opsiyonel)*
- SQL Server / PostgreSQL *(EF Core için)*

### Kurulum

```bash
git clone https://github.com/gvnuysal/gvn.gvnframework.git
cd gvn.gvnframework
dotnet build
```

### Sample API'yi Çalıştır

```bash
dotnet run --project samples/Gvn.GvnFramework.SampleApi
```

| Adres | Açıklama |
|-------|----------|
| `http://localhost:5204/scalar/v1` | 📄 Scalar API UI |
| `http://localhost:5204/openapi/v1.json` | 📋 OpenAPI JSON |
| `http://localhost:5204/hangfire` | ⏰ Hangfire Dashboard |
| `http://localhost:5204/api/health` | ✅ Health check |

---

## 🔧 Modül Detayları

### Result Pattern

```csharp
// Başarılı sonuç
var result = Result<UserDto>.Ok(userDto);

// Hatalı sonuç
var result = Result<UserDto>.Fail(Error.NotFound("USER_NOT_FOUND", "Kullanıcı bulunamadı."));

// Pattern matching
return result.Match(
    onSuccess: data => Ok(data),
    onFailure: errors => NotFound(errors)
);
```

### CQRS — Command & Query

```csharp
// Command tanımla
public record CreateUserCommand(string Name, string Email) : ICommand<Guid>;

// Handler yaz
public class CreateUserHandler(IRepository<User> repo)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand cmd, CancellationToken ct)
    {
        var user = new User(cmd.Name, cmd.Email);
        await repo.AddAsync(user, ct);
        return Result<Guid>.Ok(user.Id);
    }
}

// Controller'dan kullan
[HttpPost]
public async Task<IActionResult> Create(CreateUserCommand cmd)
    => HandleResult(await mediator.Send(cmd));
```

### Domain Events

```csharp
// Event tanımla
public record UserCreatedEvent(Guid UserId, string Email) : DomainEvent;

// AggregateRoot'ta fırlat
public class User : AggregateRoot
{
    public static User Create(string name, string email)
    {
        var user = new User(name, email);
        user.AddDomainEvent(new UserCreatedEvent(user.Id, email));
        return user;
    }
}

// Event handler
public class UserCreatedHandler : INotificationHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent evt, CancellationToken ct)
    {
        // Email gönder, log yaz vs.
        return Task.CompletedTask;
    }
}
```

### Specification Pattern

```csharp
public class ActiveUsersSpec : Specification<User>
{
    public ActiveUsersSpec(int page, int size)
    {
        Criteria = u => u.IsActive && !u.IsDeleted;
        AddInclude(u => u.Orders);
        ApplyOrderByDescending(u => u.CreatedAt);
        ApplyPaging((page - 1) * size, size);
    }
}

// Kullanım
var users = await readRepo.GetBySpecAsync(new ActiveUsersSpec(1, 10));
```

---

## ⚙️ Konfigürasyon

`appsettings.json`:

```json
{
  "Jwt": {
    "Secret": "your-super-secret-key-min-32-characters",
    "Issuer": "GvnFramework",
    "Audience": "GvnFramework.Clients",
    "ExpiryMinutes": 60
  },
  "Cache": {
    "UseRedis": true,
    "ConnectionString": "localhost:6379",
    "Prefix": "gvn:",
    "DefaultExpiryMinutes": 60
  },
  "Hangfire": {
    "UseInMemory": false,
    "SqlServerConnectionString": "Server=.;Database=HangfireDb;..."
  }
}
```

`Program.cs`:

```csharp
// Security
builder.Services.AddGvnSecurity(jwt => { ... });

// Caching
builder.Services.AddGvnCaching(cache => { ... });

// Background Jobs
builder.Services.AddGvnBackgroundJobs(hf => { ... });

// Scalar / OpenAPI
builder.Services.AddGvnSwagger("My API", "v1");

// Application (CQRS, Validation, Logging pipeline)
builder.Services.AddApplicationServices(Assembly.GetExecutingAssembly());

// Middleware
app.UseGvnCorrelationId();
app.UseGvnExceptionHandling();
app.UseGvnSwagger();
app.UseGvnHangfireDashboard();
```

---

## 📁 Proje Yapısı

```
Gvn.GvnFramework/
├── 📂 framework/src/
│   ├── 💎 Gvn.GvnFramework.Core
│   ├── 🏢 Gvn.GvnFramework.Domain
│   ├── ⚙️  Gvn.GvnFramework.Application
│   ├── 🗄️  Gvn.GvnFramework.EntityFramewokCore
│   ├── 🌐 Gvn.GvnFramework.AspNetCore
│   ├── 🔐 Gvn.GvnFramework.Security
│   ├── 📊 Gvn.GvnFramework.Logging
│   ├── 🚀 Gvn.GvnFramework.Caching
│   ├── ⏰ Gvn.GvnFramework.BackgroundJobs
│   ├── 📄 Gvn.GvnFramework.Swagger
│   ├── 🧩 Gvn.GvnFramework.Modularity
│   └── 🔩 Gvn.GvnFramework.DepedencyInjection
├── 📂 samples/
│   └── 🚀 Gvn.GvnFramework.SampleApi
└── 📂 Tests/  (coming soon)
```

---

## 🛠️ Teknolojiler

<div align="center">

| Kategori | Teknoloji | Versiyon |
|----------|-----------|----------|
| **Runtime** | .NET | 10.0 |
| **Language** | C# | 14.0 |
| **CQRS** | MediatR | 12.x |
| **Validation** | FluentValidation | 11.x |
| **ORM** | Entity Framework Core | 9.x |
| **Logging** | Serilog | 9.x |
| **Caching** | StackExchange.Redis | 2.x |
| **Background Jobs** | Hangfire | 1.8 |
| **API Docs** | Scalar + OpenAPI | 2.x |
| **Security** | JWT Bearer + BCrypt | - |

</div>

---

## 🗺️ Yol Haritası

- [x] Core modülü (Guard, Result, Exceptions)
- [x] Domain building blocks (Entity, AggregateRoot, ValueObject)
- [x] CQRS + MediatR pipeline
- [x] Entity Framework Core integration
- [x] Serilog logging
- [x] Redis caching
- [x] JWT security
- [x] Scalar API documentation
- [x] Hangfire background jobs
- [x] Sample API
- [ ] Unit & Integration Tests
- [ ] NuGet paket yayını
- [ ] Health Checks modülü
- [ ] Multi-tenancy desteği
- [ ] gRPC desteği

---

<div align="center">

**Gvn.GvnFramework** — *Clean. Modular. Production-Ready.*

[![GitHub](https://img.shields.io/badge/GitHub-gvnuysal-181717?style=for-the-badge&logo=github)](https://github.com/gvnuysal)

</div>
