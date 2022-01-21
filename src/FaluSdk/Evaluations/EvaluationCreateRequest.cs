using Falu.Core;

namespace Falu.Evaluations;

/// <summary>
/// Information for creating an evaluation.
/// </summary>
public class EvaluationCreateRequest : EvaluationPatchModel, IHasCurrency
{
    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <summary>
    /// Scope of the evaluation.
    /// </summary>
    public string? Scope { get; set; }

    /// <summary>
    /// Provider of the statement.
    /// </summary>
    public string? Provider { get; set; }

    /// <summary>
    /// Full name of the owner of the statement.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Phone number for attached to the statement.
    /// Only required for statements generated against a phone number such as <c>mpesa</c>.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Password to open the statement file.
    /// Only required for password protected files.
    /// Certain providers only provide password protected files.
    /// In such cases the password should always be provided.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Unique identifier of the file containing the sstatement
    /// </summary>
    public string? File { get; set; }
}
