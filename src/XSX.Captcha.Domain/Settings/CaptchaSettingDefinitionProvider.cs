using Volo.Abp.Settings;

namespace XSX.Captcha.Settings
{
    public class CaptchaSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(CaptchaSettings.MySetting1));
        }
    }
}
