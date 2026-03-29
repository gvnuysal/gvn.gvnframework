using Serilog.Core;
using Serilog.Events;

namespace Gvn.GvnFramework.Logging.Enrichers;

/// <summary>
/// Serilog enricher that attaches a <c>CorrelationId</c> property to every log event.
/// Uses the provided ID when available; otherwise generates a short random identifier.
/// </summary>
public sealed class CorrelationIdEnricher : ILogEventEnricher
{
    /// <summary>The name of the Serilog property added to each log event.</summary>
    public const string CorrelationIdPropertyName = "CorrelationId";

    private readonly string? _correlationId;

    /// <summary>
    /// Initializes a new instance of <see cref="CorrelationIdEnricher"/>.
    /// </summary>
    /// <param name="correlationId">
    /// An explicit correlation ID to use. If <c>null</c>, a random 8-character hex string is generated per event.
    /// </param>
    public CorrelationIdEnricher(string? correlationId = null)
    {
        _correlationId = correlationId;
    }

    /// <summary>
    /// Enriches the log event with a <c>CorrelationId</c> property if one is not already present.
    /// </summary>
    /// <param name="logEvent">The log event to enrich.</param>
    /// <param name="propertyFactory">Factory used to create the log event property.</param>
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        if (logEvent.Properties.ContainsKey(CorrelationIdPropertyName))
            return;

        var id = _correlationId ?? Guid.NewGuid().ToString("N")[..8];
        var property = propertyFactory.CreateProperty(CorrelationIdPropertyName, id);
        logEvent.AddPropertyIfAbsent(property);
    }
}
