namespace Gvn.GvnFramework.EntityFramewokCore.Outbox;

/// <summary>
/// Represents a persisted outbox message used for reliable domain event delivery.
/// Messages are written in the same transaction as the aggregate and processed
/// asynchronously by a background job.
/// </summary>
public sealed class OutboxMessage
{
    /// <summary>Gets the unique identifier of this outbox message.</summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>Gets the fully-qualified CLR type name of the domain event.</summary>
    public string EventType { get; private set; } = default!;

    /// <summary>Gets the JSON-serialized payload of the domain event.</summary>
    public string Content { get; private set; } = default!;

    /// <summary>Gets the UTC timestamp when this message was created.</summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>Gets the UTC timestamp when this message was processed, or <c>null</c> if not yet processed.</summary>
    public DateTime? ProcessedAt { get; private set; }

    /// <summary>Gets the error message if processing failed, or <c>null</c> if processing succeeded.</summary>
    public string? Error { get; private set; }

    private OutboxMessage() { }

    /// <summary>
    /// Creates a new outbox message for the given event type and serialized content.
    /// </summary>
    /// <param name="eventType">The fully-qualified CLR type name of the domain event.</param>
    /// <param name="content">The JSON-serialized domain event payload.</param>
    /// <returns>A new, unprocessed <see cref="OutboxMessage"/>.</returns>
    public static OutboxMessage Create(string eventType, string content)
        => new() { EventType = eventType, Content = content };

    /// <summary>
    /// Marks this message as successfully processed by recording the current UTC timestamp.
    /// </summary>
    public void MarkProcessed() => ProcessedAt = DateTime.UtcNow;

    /// <summary>
    /// Marks this message as failed by recording the error and setting the processed timestamp.
    /// </summary>
    /// <param name="error">A description of the error that occurred during processing.</param>
    public void MarkFailed(string error)
    {
        Error = error;
        ProcessedAt = DateTime.UtcNow;
    }
}
