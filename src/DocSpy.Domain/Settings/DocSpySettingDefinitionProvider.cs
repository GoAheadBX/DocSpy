using Volo.Abp.Settings;

namespace DocSpy.Settings
{
    public class DocSpySettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(DocSpySettings.MySetting1));
        }
    }
}
