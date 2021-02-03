using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTools.Application.Security
{
    public class UnregisteredUser
    {
        public enum UnregisteredUserType { Undefined, Employee, Customer }
        public string Id { get; set; }
        public UnregisteredUserType UserType { get; set; }
        public string Name { get; set; }
        public string OtherName { get; set; }
        public string AssignedUserName { get; set; }
        public string AssignedEmail { get; set; }
    }
}
