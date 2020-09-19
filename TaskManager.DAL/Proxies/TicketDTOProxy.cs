using CommonServiceLocator;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.DTO;
using TaskManager.BLL.Translators;

namespace TaskManager.DAL.Proxies
{
    [DataContract(Name = "ticket", Namespace = "")]
    public class TicketDTOProxy : TicketDTO
    {
        public override string StatusValue
        {
            get => ServiceLocator.Current.GetInstance<TaskManagerTranslator>().TranslateSentence(base.StatusValue);
        }
    }
}
