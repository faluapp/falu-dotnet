namespace Falu.MessageBatches;

/// <summary>
/// Represents the status of the messages in a batch.
/// </summary>
public class MessageBatchStatus
{
    /// <summary>Number of messages in the <c>accepted</c> status.</summary>
    public int Accepted { get; set; }

    /// <summary>Number of messages in the <c>sending</c> status.</summary>
    public int Sending { get; set; }

    /// <summary>Number of messages in the <c>sent</c> status.</summary>
    public int Sent { get; set; }

    /// <summary>Number of messages in the <c>failed</c> status.</summary>
    public int Failed { get; set; }

    /// <summary>Number of messages in the <c>delivered</c> status.</summary>
    public int Delivered { get; set; }

    /// <summary>Number of messages in the <c>cancelled</c> status.</summary>
    public int Cancelled { get; set; }
}
