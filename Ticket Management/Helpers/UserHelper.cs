using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TicketManagement.Models;

namespace TicketManagement.Helpers
{
    public class UserHelper
    {
        public static async Task<AppUser> getUser(HttpContext context, UserManager<AppUser> _userManager) {
            if (context != null)
            { 
                return await _userManager.GetUserAsync(context.User).ConfigureAwait(false);
            }
            return null;
        }
    }
}
