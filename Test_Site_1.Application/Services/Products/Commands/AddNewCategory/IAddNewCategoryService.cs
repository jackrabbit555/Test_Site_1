using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Site_1.Application.Services.Products.Commands.AddNewCategory
{
    public interface IAddNewCategoryService
    {
        ResultDto Execute(long? ParentId, string Name);
    }

    public class AddNewCategoryService : IAddNewCategoryService
    {
        private readonly IDataBaseContext _context;
        public AddNewCategoryService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long? ParentId, string Name)
        {
            if (string.IsNullOrEmpty(Name))
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "نام دسته بندی را وارد نمایید",
                };
            }

            Category category = new Category()
            {
                Name = Name,
                ParentCategory = GetParent(ParentId)
            };
            _context.Categories.Add(category);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "دسته بندی با موفقیت اضافه شد",
            };
        }

        private Category GetParent(long? ParentId)
        {
            return 
                _context.Categories.Find(ParentId);
        }
    }
}
