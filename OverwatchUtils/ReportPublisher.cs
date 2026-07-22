using Eth2Overwatch.Controllers;
using Eth2Overwatch.Models;
using LockMyEthTool.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eth2Overwatch.OverwatchUtils
{
    static class ReportPublisher
    {
        public static void PublishCombinedReport(IEnumerable<IProcessController> controllers)
        {
            if (controllers == null)
            {
                return;
            }

            List<IProcessController> controllerList = controllers.Where(controller => controller != null).ToList();
            if (controllerList.Count == 0)
            {
                return;
            }

            IProcessController validatorController = controllerList.FirstOrDefault(controller => controller.ProcessType == PROCESS_TYPES.VALIDATOR);
            if (validatorController == null || String.IsNullOrWhiteSpace(validatorController.ReportPath) || String.IsNullOrWhiteSpace(validatorController.ReportKey))
            {
                return;
            }

            try
            {
                ReportBody body = new ReportBody();
                body.code = validatorController.ReportKey;
                body.data.Label = validatorController.ReportLabel;
                body.data.TS = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

                foreach (IProcessController controller in controllerList)
                {
                    if (controller is IReportContributor contributor)
                    {
                        contributor.AddToReport(body.data);
                    }
                }

                WebUtils.SendData(validatorController.ReportPath, JsonConvert.SerializeObject(body));
            }
            catch
            {

            }
        }
    }
}
