using Test_Site_1.Domain.Entities.Commons;
using System.Collections.Generic;

namespace Test_Site_1.Domain.Entities.Users
{
    public class Role: BaseEntity
    {
         public string  Name { get; set; }
        public ICollection<UserInRole > UserInRoles { get; set; }
    }
}
