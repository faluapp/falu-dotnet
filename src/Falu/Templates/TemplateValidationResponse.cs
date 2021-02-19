using System.Collections.Generic;

namespace Falu.Templates
{
    /// <summary>
    /// Response for validating a template
    /// </summary>
    public class TemplateValidationResponse
    {
        /// <summary>
        /// A JSON object structure that will provide information
        /// for all keys found in the template content submitted.
        /// If a <code>Model</code> was submitted, it will be merged
        /// and returned with this model.
        /// </summary>
        public IDictionary<string, object> Model { get; set; }

        /// <summary>
        /// Using the <code>Model</code> the text content that
        /// would be produced by this template when the template
        /// content and model are combined.
        /// </summary>
        public string Rendered { get; set; }
    }
}
