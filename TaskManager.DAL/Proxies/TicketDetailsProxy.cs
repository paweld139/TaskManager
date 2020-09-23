using CommonServiceLocator;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Details;
using TaskManager.BLL.Translators;

namespace TaskManager.DAL.Proxies
{
    [DataContract(Name = "ticket", Namespace = "")]
    public class TicketDetailsProxy : TicketDetails
    {
        private TaskManagerTranslator Translator => ServiceLocator.Current.GetInstance<TaskManagerTranslator>();

        public override string StatusValue => Translator.TranslateSentence(base.StatusValue);

        public override string PriorityValue => Translator.TranslateSentence(base.PriorityValue);

        public override string TypeValue => Translator.TranslateSentence(base.TypeValue);
    }
}
