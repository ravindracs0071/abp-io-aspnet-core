using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Sample.Demo.Localization
{
    /// <summary>
    /// Used to store a localization text.
    /// </summary>
    public class ApplicationLanguageText : AuditedEntity<long>, IMultiTenant
    {
        public const int MaxSourceNameLength = 128;
        public const int MaxKeyLength = 256;
        public const int MaxValueLength = 64 * 1024 * 1024; //64KB

        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        public virtual Guid? TenantId { get; set; } //Defined by the IMultiTenant interface

        /// <summary>
        /// Language name (culture name). Matches to <see cref="ApplicationLanguage.Name"/>.
        /// </summary>
        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength)]
        public virtual string LanguageName { get; set; }

        /// <summary>
        /// Localization source name
        /// </summary>
        [Required]
        [StringLength(MaxSourceNameLength)]
        public virtual string Source { get; set; }

        /// <summary>
        /// Localization key
        /// </summary>
        [Required]
        [StringLength(MaxKeyLength)]
        public virtual string Key { get; set; }

        /// <summary>
        /// Localized value
        /// </summary>
        [Required(AllowEmptyStrings = true)]
        [StringLength(MaxValueLength)]
        public virtual string Value { get; set; }
    }
}
