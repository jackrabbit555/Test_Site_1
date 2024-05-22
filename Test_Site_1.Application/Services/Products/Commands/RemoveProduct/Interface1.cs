using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Products.Commands.RemoveProduct
{
    public interface IRemoveProductService
    {
        ResultDto Execute(long? Id);
    }

    public class RemoveProductService : IRemoveProductService
    {
        private readonly IDataBaseContext _context;
        public RemoveProductService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto Execute(long? Id)
        {
            var product = _context.Products.Find(Id);
            if (product == null)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کالایی انتخاب نشده "
                };
            }
            product.RemoveTime = DateTime.Now;
            product.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "کالا با موفقیت حذف شد"
            };
        }


    }


}
