using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Finances.Queries.GetRequestPayService
{
    public interface IGetRrquestPayForAdminService
    {
        ResultDto<List<RequestPayDto>> Execute();
    }

    public class GetRrquestPayForAdminService : IGetRrquestPayForAdminService
    {
        private readonly IDataBaseContext _context;


        public GetRrquestPayForAdminService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<RequestPayDto>> Execute()
        {
            var requestPay = _context.RequestPays.Include(x => x.User).ToList().Select(x => new RequestPayDto
            {
                Id = x.Id,
                Amount = x.Amount,
                Authority = x.Authority,
                Guid = x.Guid,
                IsPay = x.IsPay,
                PayDate = x.PayDate,
                RefId = x.RefId,
                UserId = x.UserId,
                UserName = x.User.FullName,
            }).ToList();



            return new ResultDto<List<RequestPayDto>>()
            {
                Data = requestPay,
                IsSuccess = true,

            };

        }
    }



    public class RequestPayDto
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public long UserId { get; set; }
        public int Amount { get; set; }
        public bool IsPay { get; set; }
        public DateTime? PayDate { get; set; }
        public string Authority { get; set; }
        public long RefId { get; set; } = 0;
    }
}
