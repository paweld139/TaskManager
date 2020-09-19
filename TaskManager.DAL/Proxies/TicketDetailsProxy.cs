using CommonServiceLocator;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Details;
using TaskManager.BLL.Translators;

namespace TaskManager.DAL.Proxies
{
    [DataContract(Name = "ticket", Namespace = "")]
    public class TicketDetailsProxy : TicketDetails
    {
        public override string StatusValue
        {
            get => ServiceLocator.Current.GetInstance<TaskManagerTranslator>().TranslateSentence(base.StatusValue);
        }
    }
}
