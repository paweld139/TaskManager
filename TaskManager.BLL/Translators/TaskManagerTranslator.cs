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
            ["Zakończone"] = () => Resources.Common.Ended,

            ["Nieokreślony"] = () => Resources.Common.Indefinite,
            ["Nieznany"] = () => Resources.Common.Unknown,
            ["Niski"] = () => Resources.Common.Low,
            ["Średni"] = () => Resources.Common.Medium,
            ["Wysoki"] = () => Resources.Common.High,

            ["Serwis"] = () => Resources.Common.Service,
            ["Z wyceny"] = () => Resources.Common.FromValuation,
            ["Handlowe"] = () => Resources.Common.Commercial,
            ["Wewnętrzne"] = () => Resources.Common.Internal,

            ["Passwords must have at least one non letter or digit character."] = () => Resources.ErrorMessages.PasswordSymbolRequred,
            ["Passwords must have at least one digit ('0'-'9')."] = () => Resources.ErrorMessages.PasswordDigitRequired,
            ["Passwords must have at least one uppercase ('A'-'Z')."] = () => Resources.ErrorMessages.PasswordUppercaseRequired,
            ["Passwords must have at least one lowercase ('a'-'z')."] = () => Resources.ErrorMessages.PasswordLowercaseRequired,
            ["Invalid token."] = () => Resources.Common.InvalidToken,
            ["is already taken"] = () => Resources.ErrorMessages.IsAlreadyTaken
        };

        protected override Dictionary<string, Func<string>> Words => new Dictionary<string, Func<string>>
        {
            ["Name"] = () => Resources.Common.Name,
            ["already"] = () => Resources.Common.already,
            ["taken"] = () => Resources.Common.taken
        };
    }
}
