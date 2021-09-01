using Falu.Core;
using System.IO;

namespace Falu.Evaluations
{
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
        public EvaluationScope? Scope { get; set; }

        /// <summary>
        /// Provider of the statement.
        /// </summary>
        public StatementProvider? Provider { get; set; }

        /// <summary>
        /// Full name of the owner of the statement.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Phone number for attached to the statement.
        /// Only required for statements generated against a phone number such as <see cref="StatementProvider.Mpesa"/>
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Password to open the uploaded file.
        /// Only required for password protected files.
        /// Certain providers only provide password protected files.
        /// In such cases the password should always be provided.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// The name of the file when uploading from a browser form
        /// </summary>
        public string FileName { get; set; } = "statement.pdf";

        /// <summary>
        /// The stream content of the statement file.
        /// </summary>
        public Stream? Content { get; set; }
    }
}
