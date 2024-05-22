using Microsoft.EntityFrameworkCore;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Products.Queries.GetProductForAdmin
{
    public interface IGetProductForAdminService
    {

        ResultDto<ProductForAdminDto> Execute(int page = 1, int pagesize = 20);
    }

    public class GetProductForAdminService : IGetProductForAdminService
    {
        private readonly IDataBaseContext _context;

        public GetProductForAdminService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductForAdminDto> Execute(int page = 1, int pagesize = 20)
        {
            int rowCaount = 0;
            var products = _context.Products.
                Include(p => p.Category).
                ToPaged(page, pagesize, out rowCaount).
                Select(p => new ProductForAdminList_Dto
            {
                Id = p.Id,
                Brand = p.Brand,
                Category = p.Category.Name,
                Description = p.Description,
                Displayed = p.Display,
                Inventory = p.Inventory,
                Name = p.Name,
                Price = p.Price,





            }
            ).ToList();


            return new ResultDto<ProductForAdminDto>()
            {
                Data = new ProductForAdminDto()
                {
                    Products = products,
                    CurrentPage = page,
                    RowCount = rowCaount,
                    PageSize = pagesize
                },
                IsSuccess = true,
                Message = "",


            };
        }




    }


    public class ProductForAdminDto
    {
        public int RowCount { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }


        public List<ProductForAdminList_Dto> Products { get; set; }
    }

    public class ProductForAdminList_Dto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
    }

}
