using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Products.Queries.GetAllCategories
{
    public interface IGetAllCategoriesService
    {

        ResultDto<List<AllCategoriesDto>> Execute();
    }


    public class GetAllCategoriesService : IGetAllCategoriesService 
    {
        private readonly IDataBaseContext _context;
        public GetAllCategoriesService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<List<AllCategoriesDto>> Execute() 
        
        {


            var categories = _context.Categories
                .Include(p=>p.ParentCategory)
                .Where(p=>p.ParentCategoryId!=null)
                .ToList()
                .Select(p=> new AllCategoriesDto 
            {
                Id = p.Id,
                    Name = p.ParentCategory != null ? $"{p.ParentCategory.Name}-{p.Name}" : p.Name,
                }
            ).ToList();
            return new ResultDto<List<AllCategoriesDto>>
            {
                Data = categories,
                IsSuccess = false,
                Message = ""

            };
        }




    }



    public class AllCategoriesDto 
    {
        public long Id { get; set; }
        public string Name { get; set; }    
    }
}
