using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.ProductMessage.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Widgets.ProductMessage.Components
{
    [ViewComponent(Name = "WidgetsProductMessage")]
    public class WidgetsProductMessageViewComponent : NopViewComponent
    {
        private readonly ISettingService _settingService;

        public WidgetsProductMessageViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            var productMessageSettings = _settingService.LoadSetting<ProductMessageSettings>();

            var model = new PublicInfoModel
            {
                Message = productMessageSettings.Message
            };

            if (string.IsNullOrEmpty(model.Message))
                return Content("");

            return View("~/Plugins/Widgets.ProductMessage/Views/PublicInfo.cshtml", model);
        }
    }
}
