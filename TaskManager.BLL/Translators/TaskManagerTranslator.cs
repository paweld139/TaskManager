using PDCore.Helpers.Translation;
using System;
using System.Collections.Generic;

namespace TaskManager.BLL.Translators
{
    public class TaskManagerTranslator : Translator
    {
        protected override Dictionary<string, Func<string>> Sentences => new Dictionary<string, Func<string>>
        {
            ["Przyjęte"] = () => Resources.Common.New,
            ["Do odbioru"] = () => Resources.Common.Receipt,
            ["Do wyjaśnienia"] = () => Resources.Common.Clarify,
            ["Anulowane"] = () => Resources.Common.Canceled,
            ["Odrzucone"] = () => Resources.Common.Rejected,
            ["Zakończone"] = () => Resources.Common.Ended
        };

        protected override Dictionary<string, Func<string>> Words => new Dictionary<string, Func<string>>();
    }
}
