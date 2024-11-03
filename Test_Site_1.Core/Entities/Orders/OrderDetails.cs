using Test_Site_1.Domain.Entities.Commons;
using Test_Site_1.Domain.Entities.Products;

namespace Test_Site_1.Domain.Entities.Orders
{
    public class OrderDetail :BaseEntity 
    {
        public virtual Order Order { get; set; }
        public long OrderId { get; set; } = 0;
        public virtual Product Product { get; set; }
        public long ProductId { get; set; } 
        public int Price {  get; set; }
        public int Count { get; set; }

    }
}
