using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TaskManager.Web.Controllers
{
    [Authorize(Roles = "Serwisant, Admin")]
    public class ReportsController : Controller
    {
        public ActionResult GetBudgetByContractorsReport()
        {
            return Redirect("~/Reports/BudgetByContractors/BudgetByContractors.aspx");
        }
    }
}
