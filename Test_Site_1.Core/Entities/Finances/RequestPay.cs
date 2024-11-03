using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Domain.Entities.Commons;
using Test_Site_1.Domain.Entities.Orders;
using Test_Site_1.Domain.Entities.Users;

namespace Test_Site_1.Domain.Entities.Finances
{
    public class RequestPay:BaseEntity
    
    {
        public Guid Guid { get; set; }
        public virtual User User { get; set; }
        public long UserId { get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
