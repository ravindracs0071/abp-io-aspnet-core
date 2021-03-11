using Sample.Demo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Sample.Demo.Permissions
{
    public class DemoPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            //Define your own permissions here. Example:
            //myGroup.AddPermission(DemoPermissions.MyPermission1, L("Permission:MyPermission1"));

            var demoGroup = context.AddGroup(DemoPermissions.GroupName, L("Permission:Demo"));

            var booksPermission = demoGroup.AddPermission(DemoPermissions.Incidents.Default, L("Permission:Incidents"));
            booksPermission.AddChild(DemoPermissions.Incidents.Create, L("Permission:Incidents.Create"));
            booksPermission.AddChild(DemoPermissions.Incidents.Edit, L("Permission:Incidents.Edit"));
            booksPermission.AddChild(DemoPermissions.Incidents.Delete, L("Permission:Incidents.Delete"));

            //TODO Additional permissions
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<DemoResource>(name);
        }
    }
}
