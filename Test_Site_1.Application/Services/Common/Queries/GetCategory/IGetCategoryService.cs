using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Common.Queries.GetCategory
{
    public interface IGetCategoryService
    {

        ResultDto<List<CategoryDto>> Execute();
        
   
    }

    public class GetCategoryService : IGetCategoryService 
    {
        private readonly IDataBaseContext _context;
        public GetCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<CategoryDto>> Execute() 
        {
            try
            {

                var category = _context.Categories.Where(p=>p.ParentCategory == null)
                    .ToList()
                    .Select(p=>new CategoryDto 
                    {
                        CatId = p.Id,
                        CategoryName = p.Name,
                        
                    }).ToList();
                return new ResultDto<List<CategoryDto>>()
                {
                    Data = category,
                    IsSuccess = true
                };

            }
            catch (Exception ex)
            {
                var ErrorMessage = ex.Message;
                var InnerErrorMessage = ex.InnerException?.Message;

                return new ResultDto<List<CategoryDto>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ErrorMessage + (InnerErrorMessage != null ? " + " + InnerErrorMessage : string.Empty)

                };

            }
        }
    }


    public class CategoryDto 
    {
        public long CatId { get; set; }
        public string CategoryName { get; set; }    

    }
}
