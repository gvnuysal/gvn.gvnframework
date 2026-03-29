using Serilog.Core;
using Serilog.Events;

namespace Gvn.GvnFramework.Logging.Enrichers;

public sealed class CorrelationIdEnricher : ILogEventEnricher
{
    public const string CorrelationIdPropertyName = "CorrelationId";

    private readonly string? _correlationId;

    public CorrelationIdEnricher(string? correlationId = null)
    {
        _correlationId = correlationId;
    }

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Properties.ContainsKey(CorrelationIdPropertyName))
            return;

        var id = _correlationId ?? Guid.NewGuid().ToString("N")[..8];
        var property = propertyFactory.CreateProperty(CorrelationIdPropertyName, id);
        logEvent.AddPropertyIfAbsent(property);
    }
}
