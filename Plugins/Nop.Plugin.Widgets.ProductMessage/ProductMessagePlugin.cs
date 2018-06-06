using Nop.Core;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductMessage
{
    public class ProductMessagePlugin : BasePlugin, IWidgetPlugin
    {
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;

        public ProductMessagePlugin(ISettingService settingService, IWebHelper webHelper)
        {
            _settingService = settingService;
            _webHelper = webHelper;
        }
        public void GetPublicViewComponent(string widgetZone, out string viewComponentName)
        {
            viewComponentName = "WidgetsProductMessage";
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string> { "productdetails_after_breadcrumb" };
        }

        public override string GetConfigurationPageUrl()
        {
            return _webHelper.GetStoreLocation() + "Admin/WidgetsProductMessage/Configure";
        }

        public override void Install()
        {

            var settings = new ProductMessageSettings
            {
                Message = "50% discount in December"
            };

            _settingService.SaveSetting(settings);

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMessage.Message", "Message");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.ProductMessage.Message.Hint", "Enter a message to display on product details page.");

            base.Install();
        }

        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<ProductMessageSettings>();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.ProductMessage.Message");
            this.DeletePluginLocaleResource("Plugins.Widgets.ProductMessage.Message.Hint");

            base.Uninstall();
        }
    }
}
