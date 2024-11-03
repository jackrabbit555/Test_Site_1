using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.Finances;

namespace Test_Site_1.Application.Services.Finances.Command.AddReuestPay
{
    public interface IAddRequestpayService
    {
        ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId);
    }


    public class AddRequestpayService : IAddRequestpayService
    {

        private readonly IDataBaseContext _context;
        public AddRequestpayService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultRequestPayDto> Execute(int Amount, long UserId)
        {


            var user = _context.Users.Find(UserId);
            if (user == null)
            {
                return new ResultDto<ResultRequestPayDto>()
                {
                    IsSuccess = false,
                    Message = "کاربر مورد نظر یافت نشد."
                };
            }
            else 
            {
                RequestPay requestPay = new RequestPay()
                {
                    Amount = Amount,
                    Guid = Guid.NewGuid(),
                    IsPay = false,
                    User = user
                };
                _context.RequestPays.Add(requestPay);
                _context.SaveChanges();
                return new ResultDto<ResultRequestPayDto>()
                {
                    Data = new ResultRequestPayDto
                    {
                        guid = requestPay.Guid,
                        Amount = requestPay.Amount,
                        Email = user.Email,
                        RequestPayId = requestPay.Id,

                    },
                    IsSuccess = true,

                };
            }
          
        }

    }

    public class ResultRequestPayDto
    {
        public Guid guid { get; set; }
        public int Amount { get; set; }
        public string Email { get; set; }
        public long RequestPayId { get; set; }
    }
}
