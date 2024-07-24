using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Products.Queries.GetProductForSite
{
    public interface IGetProductForSiteService
    {
        ResultDto<ResultProductForSiteDto> Execute( string SearchKey , int page , long? CatId );

    }

    public class GetProductForSiteService : IGetProductForSiteService 
    {
        private readonly IDataBaseContext _context;

        public GetProductForSiteService(IDataBaseContext context)
        {
                _context = context;
        }

        public ResultDto<ResultProductForSiteDto> Execute(string SearchKey,int page, long? CatId) 
        {
            int totalrow = 0;
            Random rnd = new Random();
            var productQuery = _context.Products
                .Include(p => p.ProductImages).AsQueryable();
            if (CatId!=null)
            {
                productQuery = productQuery.Where(p=>p.CategoryId == CatId||p.Category.ParentCategoryId == CatId).AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(SearchKey))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(SearchKey) || p.Brand.Contains(SearchKey)).AsQueryable();
            }

            var product = productQuery.ToPaged(page, 5, out totalrow);

            return new ResultDto<ResultProductForSiteDto>
            {
                Data = new ResultProductForSiteDto
                {
                    TotalRow = totalrow,
                    Products = product.Select(p => new ProductForSiteDto
                    {
                        Id = p.Id,
                        Star = rnd.Next(1, 5),
                        Title = p.Name,
                        ImageSrc = p.ProductImages.FirstOrDefault().Src,
                        Price = p.Price,
                    }).ToList()
                },
                IsSuccess = true

            };

        }
    }

    public class ProductForSiteDto 
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageSrc { get; set; }
        public int Star { get; set; }
        public int Price { get; set; }
    }
    public class ResultProductForSiteDto
    {

        public List<ProductForSiteDto> Products { get; set; }
        public int TotalRow { get; set; }
    }

}
