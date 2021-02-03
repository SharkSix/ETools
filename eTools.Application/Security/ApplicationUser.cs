using Microsoft.AspNet.Identity.EntityFramework;

namespace eTools.Application.Security
{
    public class ApplicationUser : IdentityUser
    {
        public int? EmployeeId { get; set; }
        public string CustomerId { get; set; }
    }
}
