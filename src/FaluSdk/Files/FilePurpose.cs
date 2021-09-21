using System.Runtime.Serialization;

namespace Falu.Files
{
    /// <summary>
    /// Purpose for a file.
    /// </summary>
    public enum FilePurpose
    {
        /// <summary>A business icon.</summary>
        [EnumMember(Value = "business.icon")]
        BusinessIcon,

        /// <summary>A business logo.</summary>
        [EnumMember(Value = "business.logo")]
        BusinessLogo,

        /// <summary>Customer signature image.</summary>
        [EnumMember(Value = "customer.signature")]
        CustomerSignature,

        /// <summary>Image of a selfie collected for identification purposes.</summary>
        [EnumMember(Value = "customer.selfie")]
        CustomerSelfie,

        /// <summary>Customer provided tax document.</summary>
        [EnumMember(Value = "customer.tax.document")]
        CustomerTaxDocument,

        /// <summary>Customer provided document for financial evaluation.</summary>
        [EnumMember(Value = "customer.evaluation")]
        CustomerEvaluation,

        /// <summary>Document to verify the identity of an entity.</summary>
        [EnumMember(Value = "identity.document")]
        IdentityDocument,
    }
}
