using PDWebCore;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskManager.DAL.Contracts;

namespace TaskManager.Web.Helpers
{
    public static class Extensions
    {
        public static async Task<SelectList> GetSelectList(this IContrahentRepository contrahentRepository)
        {
            var kvp = await contrahentRepository.GetKeyValuePairsAsync();

            return kvp.ToSelectList();
        }
    }
}
