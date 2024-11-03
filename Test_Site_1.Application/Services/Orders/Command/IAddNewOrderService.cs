using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.Finances;
using Test_Site_1.Domain.Entities.Orders;
using Test_Site_1.Domain.Entities.Users;

namespace Test_Site_1.Application.Services.Orders.Command
{
    public  interface IAddNewOrderService
    {
        ResultDto Execute(RequestAddNewOrderSericeDto request);

    }


    public class AddNewOrderService : IAddNewOrderService
    {
        private readonly IDataBaseContext _context;


        public AddNewOrderService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewOrderSericeDto request)
        {
            var user = _context.Users.Find(request.UserId);
            var requestPay = _context.RequestPays.Find(request.RequestPayId);
            var cart = _context.Carts.Include(c => c.cartItems)
                .ThenInclude(p => p.Product)
                .Where(p => p.Id == request.CartId)
                .FirstOrDefault();

            requestPay.IsPay = true;
            requestPay.PayDate = DateTime.Now;
            requestPay.RefId = request.RefId;
            requestPay.Authority = request.Authority;
            cart.Finished = true;

            Order order = new Order()
            {
                Address = "",
                OrderState = OrderState.Processing,
                RequestPay = requestPay,
                User = user

            };
            _context.Orders.Add(order);

            List<OrderDetail> orderdetails = new List<OrderDetail>();
            foreach (var item in cart.cartItems)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Count = item.Count,
                    Order = order,
                    Price = item.Product.Price,
                    Product = item.Product,
                };
                orderdetails.Add(orderDetail);
            }


            _context.OrderDetails.AddRange(orderdetails);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = ""
            };
        }
       
        
        
    }

    public class RequestAddNewOrderSericeDto 
    {
        public long CartId { get; set; }
        public long RequestPayId { get; set; }
        public long UserId { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
    }

}
