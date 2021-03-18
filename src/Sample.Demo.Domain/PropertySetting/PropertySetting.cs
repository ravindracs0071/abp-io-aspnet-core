using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Sample.Demo.PropertySetting
{
    public class PropertySetting : Entity<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; internal set; }

        [NotNull]
        public virtual string Name { get; protected set; }

        [NotNull]
        public virtual string Value { get; internal set; }

        [CanBeNull]
        public virtual string ProviderName { get; protected set; }

        [CanBeNull]
        public virtual string ProviderKey { get; protected set; }

        [NotNull]
        public virtual bool Visible { get; internal set; }

        [NotNull]
        public virtual bool RequiredRegEx { get; internal set; }

        [CanBeNull]
        public virtual string RegExRule { get; internal set; }

        protected PropertySetting()
        {

        }

        public PropertySetting(
            Guid id,
            [NotNull] string name,
            [NotNull] string value,
            [NotNull] bool visible,
            [NotNull] bool requiredRegEx,
            [NotNull] string providerName = null,
            [NotNull] string providerKey = null,
            [CanBeNull] string regExRule = null)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(value, nameof(value));

            Id = id;
            Name = name;
            Value = value;
            Visible = visible;
            RequiredRegEx = requiredRegEx;
            ProviderName = providerName;
            ProviderKey = providerKey;
            RegExRule = regExRule;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Name = {Name}, Value = {Value}, ProviderName = {ProviderName}, ProviderKey = {ProviderKey}";
        }
    }
}