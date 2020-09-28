using CommonServiceLocator;
using System.Runtime.Serialization;
using TaskManager.BLL.Entities.Briefs;
using TaskManager.BLL.Translators;

namespace TaskManager.DAL.Proxies
{
    [DataContract(Name = "dictionary", Namespace = "")]
    public class DictionaryBriefProxy  : DictionaryBrief
    {
        private TaskManagerTranslator Translator => ServiceLocator.Current.GetInstance<TaskManagerTranslator>();

        public override string Value { get => Translator.TranslateSentence(base.Value); }
    }
}
