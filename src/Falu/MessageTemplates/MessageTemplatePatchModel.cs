using Falu.Core;
using System.Collections.Generic;

namespace Falu.MessageTemplates
{
    /// <summary>
    /// A model representing details that can be changed about a template
    /// </summary>
    public class MessageTemplatePatchModel : IHasDescription, IHasMetadata, IHasTags
    {
        /// <summary>
        /// An optional string you can provide to identify this template.
        /// Allowed characters are numbers, ASCII letters, and ‘.’, ‘-’, ‘_’ characters,
        /// and the string has to start with a letter.
        /// </summary>
        public string? Alias { get; set; }

        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <summary>
        /// The content to use when this template is used to send messages.
        /// See our template language documentation for more information on the syntax for this field.
        /// </summary>
        public string? Body { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }

        /// <inheritdoc/>
        public List<string>? Tags { get; set; }
    }
}
