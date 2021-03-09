using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Sample.Demo.Localization
{
    /// <summary>
    /// Represents a language of the application.
    /// </summary>
    public class ApplicationLanguage : FullAuditedEntity<int>, IMultiTenant
    {
        /// <summary>
        /// The maximum name length.
        /// </summary>
        public const int MaxNameLength = 128;

        /// <summary>
        /// The maximum display name length.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// The maximum icon length.
        /// </summary>
        public const int MaxIconLength = 128;

        /// <summary>
        /// TenantId of this entity. Can be null for host.
        /// </summary>
        public Guid? TenantId { get; set; } //Defined by the IMultiTenant interface


        /// <summary>
        /// Gets or sets the name of the culture, like "en" or "en-US".
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        [StringLength(MaxIconLength)]
        public virtual string Icon { get; set; }

        /// <summary>
        /// Is this language active. Inactive languages are not get by <see cref="IApplicationLanguageManager"/>.
        /// </summary>
        public virtual bool IsDisabled { get; set; }

        /// <summary>
        /// Creates a new <see cref="ApplicationLanguage"/> object.
        /// </summary>
        public ApplicationLanguage()
        {
        }

        public ApplicationLanguage(Guid? tenantId, string name, string displayName, string icon = null, bool isDisabled = false)
        {
            TenantId = tenantId;
            Name = name;
            DisplayName = displayName;
            Icon = icon;
            IsDisabled = isDisabled;
        }

        public virtual LanguageInfo ToLanguageInfo()
        {
            // TODO: uiCultureName 2nd Param
            //string cultureName, string uiCultureName = null, string displayName = null, string flagIcon = null
            return new LanguageInfo(Name, Name, DisplayName, Icon);
        }
    }
}
