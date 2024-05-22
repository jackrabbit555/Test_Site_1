using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Products.Commands.RemoveCategory
{
    public interface IRemoveCategoryService
    {
        ResultDto Execute(long? CategoryId);
    }

    public class RemoveCategoryService : IRemoveCategoryService
    {
        private readonly IDataBaseContext _context;

        public RemoveCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long? CategoryId)
        {
            var category = _context.Categories.Find( CategoryId);
            //var category = _context.Categories.FirstOrDefault(c=>c.Id == CategoryId);

            if (category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "دسته بندی یافت نشد "
                };              
            }

            category.RemoveTime = DateTime.Now;
            category.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت حذف شد "
            };
        }
    }
}
