using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Plugin.Widgets.ProductMessage.Models;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductMessage.Controllers
{
    [Area(AreaNames.Admin)]
    public class WidgetsProductMessageController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public WidgetsProductMessageController(IWorkContext workContext,
            IPermissionService permissionService,
            ISettingService settingService,
            ILocalizationService localizationService)
        {
            _workContext = workContext;
            _permissionService = permissionService;
            _settingService = settingService;
            _localizationService = localizationService;
        }

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();
            
            var productMessageSettings = _settingService.LoadSetting<ProductMessageSettings>();

            var model = new ConfigurationModel()
            {
                Message = productMessageSettings.Message
            };


            return View("~/Plugins/Widgets.ProductMessage/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();
            
            var productMessageSettings = _settingService.LoadSetting<ProductMessageSettings>();

            productMessageSettings.Message = model.Message;

            _settingService.SaveSetting(productMessageSettings);

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));
            return Configure();
        }
    }
}
