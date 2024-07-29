using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Products.Queries.GetProductDeatilForSite
{
    public interface IGetProductDeatilForSiteService
    {
        ResultDto<ProductDetailForSiteDto> Execute(long Id);

    }

    public class GetProductDeatilForSiteService : IGetProductDeatilForSiteService 
    {
        private readonly IDataBaseContext _context;
        public GetProductDeatilForSiteService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductDetailForSiteDto> Execute(long Id) 
        {
            var Product = _context.Products.Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductFeatures)
                .Where(p => p.Id == Id).FirstOrDefault();
            if (Product == null)
            {
                throw new Exception("Product NotFound ..... ");
            }

            Product.ViewCount++;
            _context.SaveChanges();
            return new ResultDto<ProductDetailForSiteDto>()
            {
                Data = new ProductDetailForSiteDto
                {
                    Brand = Product.Brand,
                    Category = $"{Product.Category.ParentCategory.Name} - {Product.Category.Name}",
                    Description = Product.Description,
                    Id = Product.Id,
                    Price = Product.Price,
                    Title = Product.Name,
                    Images = Product.ProductImages.Select(p => p.Src).ToList(),
                    Features = Product.ProductFeatures.Select(p => new ProductDetailForSite_FeaturesDto
                    {
                        DispalyName = p.DisplayName,
                        Value = p.Value,

                    }).ToList(),

                },
                IsSuccess = true,

            };


        }

        
    }


    public class ProductDetailForSiteDto 
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }    
        public List<string> Images { get; set; }
        public List<ProductDetailForSite_FeaturesDto> Features { get; set; }
    }


    public class ProductDetailForSite_FeaturesDto 
    {
        public string DispalyName { get; set; }
        public string Value { get; set; }
    }


}
