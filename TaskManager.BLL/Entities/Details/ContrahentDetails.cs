using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.BLL.Entities.Basic;

namespace TaskManager.BLL.Entities.Details
{
    public class ContrahentDetails : ContrahentBasic
    {
        public string RoleName => IsOperator ? "Serwisant" : "Klient";
    }
}
