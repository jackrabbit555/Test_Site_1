using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Domain.Entities.Commons;
using Test_Site_1.Domain.Entities.Products;
using Test_Site_1.Domain.Entities.Users;

namespace Test_Site_1.Domain.Entities.Carts
{
    public  class Cart:BaseEntity
    {
        public User User { get; set; }
        public long? UserId { get; set; }
        public Guid BrowserId { get; set; }
        public bool Finished { get; set; }
        public ICollection <CartItem> cartItems { get; set; }

    }
    public class CartItem : BaseEntity 
    {
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public virtual Cart Cart { get; set; }
        public long CartId { get; set; }

    }
}
