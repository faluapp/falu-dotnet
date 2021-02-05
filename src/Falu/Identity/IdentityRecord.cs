using Falu.Core;
using System;
using System.Collections.Generic;

namespace Falu.Identity
{
    /// <summary>
    /// The identification record for an entity.
    /// </summary>
    public class IdentityRecord : IHasId, IHasCreated, IHasUpdated, IHasCountry, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <inheritdoc/>
        public string Country { get; set; } = "ken";

        /// <summary>
        /// The kind of identification document.
        /// </summary>
        public IdentityDocumentKind? DocumentType { get; set; }

        /// <summary>
        /// The identification document number.
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// The full name of the entity.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date of birth of the entity if specified.
        /// This value is not guaranteed to be availed.
        /// For entities that are not human (e.g. companies, businesses, NGOs,
        /// this value represents the date of registration or incorporation.
        /// </summary>
        public DateTimeOffset? Birthday { get; set; }

        /// <summary>
        /// Phone number(s) attached to the identity.
        /// </summary>
        public List<string> Phones { get; set; }

        /// <summary>
        /// The gender of the entity.
        /// This value may be fixed or predicted.
        /// When predicted <see cref="GenderConfidence"/> will have a value.
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// The confidence for the value predicted in <see cref="Gender"/>.
        /// This value is null when the gender is fixed and not predicted.
        /// It should not be used to determine predicted gender.
        /// </summary>
        public float? GenderConfidence { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }
    }
}
