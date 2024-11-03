using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.Orders;

namespace Test_Site_1.Application.Services.Orders.Queries.GetOrdersForAdmin
{
    public  interface IGetOrdersForAdminService
    {

        ResultDto<List<OrderDto>> Execute(OrderState orderState);
    }

    public class GetOrdersForAdminService : IGetOrdersForAdminService
    {
        private readonly IDataBaseContext _context;


        public GetOrdersForAdminService(IDataBaseContext context)
        {
             _context = context;
        }
        public ResultDto<List<OrderDto>> Execute(OrderState orderState)
        {
            var orders = _context.Orders.Include(o => o.OrderDetails)
                .Where(o => o.OrderState == orderState)
                .OrderByDescending(o => o.Id)
                .ToList()
                .Select(o => new OrderDto
                {
                    InsetTime = DateTime.Now,
                    OrderId = o.Id,
                    ProductCount = o.OrderDetails.Count(),
                    RequestId = o.RequestPayId,
                    UserId = o.UserId
                }).ToList();

            return new ResultDto<List<OrderDto>>()
            {
                Data = orders,
                IsSuccess = true
            };
            
        }
    }



    public class OrderDto 
    {
        public long OrderId { get; set; }
        public DateTime InsetTime { get; set; }
        public long RequestId { get; set; }
        public long UserId { get; set; }
        public OrderState OrderState { get; set; }
        public int ProductCount { get; set; }
    }
}
