using Eth2Overwatch.Models;

namespace Eth2Overwatch.Controllers
{
    interface IReportContributor
    {
        void AddToReport(ReportData data);
    }
}
