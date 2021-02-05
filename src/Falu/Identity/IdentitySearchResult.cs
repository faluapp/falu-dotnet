using System;
using System.Collections.Generic;
using Tingle.Extensions.Modeling.Identity;

namespace Falu.Identity
{
    /// <summary>
    /// Result of searching for an entity
    /// </summary>
    public class IdentitySearchResult
    {
        /// <summary>
        /// The full name of the entity
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The national identity number
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// The date of birth of the entity if specified.
        /// This value is not guaranteed to be availed. For entities that are not human (e.g. companies, businesses, NGOs,
        /// this value represents the date of registration or incorporation.
        /// </summary>
        public DateTimeOffset? Birthday { get; set; }

        /// <summary>
        /// The date of registration of the details provided.
        /// For example, if the record comes from a phone number registration, the value specified is the date the phone was registered.
        /// If the record comes from elsewhere, the date represents the when the record was verified or acquired.
        /// </summary>
        public DateTimeOffset? RegistrationDate { get; set; }

        /// <summary>
        /// Phone numbers attached to the person
        /// </summary>
        public List<string> PhoneNumbers { get; set; }

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
    }
}
