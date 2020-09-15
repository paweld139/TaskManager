using PDCoreNew.Configuration.DbConfiguration.Interceptors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Configuration
{
    internal class TaskManagerDbConfiguration : DbConfiguration
    {
        protected internal TaskManagerDbConfiguration()
        {
            AddInterceptor(new UtcInterceptor());
        }
    }
}
