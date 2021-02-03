using Microsoft.AspNet.Identity.EntityFramework;

namespace eTools.Application.Security
{
    internal class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
    }
}
