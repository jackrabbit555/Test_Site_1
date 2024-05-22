using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Products.Commands.EditCategory
{
    public interface IEditCategoryService
    {
        ResultDto Execute(RequestEditCategoryDto request);

    }

    public class EditCategoryService : IEditCategoryService
    {
        private readonly IDataBaseContext _context;
        public EditCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditCategoryDto request)
        {
            var category = _context.Categories.Find(request.CategoryID);
            if (category == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "دسته بندی یافت نشد "
                };
            }
            category.Name = request.CategoryName;
            _context.SaveChanges();
            return new ResultDto
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت  ویرایش شد "
            };


        }
    }


    public class RequestEditCategoryDto
    {
        public long? CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
