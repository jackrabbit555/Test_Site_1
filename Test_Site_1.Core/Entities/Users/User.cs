using Test_Site_1.Domain.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Domain.Entities.Orders;

namespace Test_Site_1.Domain.Entities.Users
{
    public class User : BaseEntity 
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public ICollection<UserInRole > UserInRoles { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
