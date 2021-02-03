using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Application.Security
{
    internal static class SecurityRoles
    {
        public const string WebsiteAdmins = "WebsiteAdmins";
        public const string Customers = "Customers";
        public const string Employees = "Employees";
        public static List<string> StartupSecurityRoles
        {
            get
            {
                List<string> value = new List<string>();
                value.Add(WebsiteAdmins);
                value.Add(Customers);
                value.Add(Employees);
                return value;
            }
        }
    }
}
