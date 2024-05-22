using Microsoft.EntityFrameworkCore;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.Products;

namespace Test_Site_1.Application.Services.Products.Queries.GetProductDetailForAdmin
{
    public interface IGetProductDetailForAdmin
    {

        ResultDto<ProductDetailForAdmindto> Execute(long Id);
    }

    public class GetProductDetailForAdmin : IGetProductDetailForAdmin
    {
        private readonly IDataBaseContext _context;

        public GetProductDetailForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductDetailForAdmindto> Execute(long Id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductFeatures)
                .Include(p => p.ProductImages)
                .Where(p => p.Id == Id)
                .FirstOrDefault();
            return new ResultDto<ProductDetailForAdmindto>()
            {
                Data = new ProductDetailForAdmindto
                {
                    Brand = product.Brand,
                    Category = GetCategory(product.Category),
                    Description = product.Description,
                    Id = product.Id,
                    Inventory = product.Inventory,
                    Name = product.Name,
                    Price = product.Price,
                    Features = product.ProductFeatures.ToList().Select(PF => new ProductDetailFeatureDto
                    {
                        Id = PF.Id,
                        DisplayName = PF.DisplayName,
                        Value = PF.Value,
                    }).ToList(),
                    Images = product.ProductImages.ToList().Select(PI => new ProductDetailImagesDto
                    {
                        Id = PI.Id,
                        Src = PI.Src,
                    }).ToList()
                },
                IsSuccess = true,
                Message = ""

            };


        }
        private string GetCategory(Category category)
        {
            string result = category.ParentCategory != null ? $"{category.ParentCategory.Name} - " : "";
            return result += category.Name;
        }
    }



    public class ProductDetailForAdmindto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }
        public List<ProductDetailFeatureDto> Features { get; set; }
        public List<ProductDetailImagesDto> Images { get; set; }
    }

    public class ProductDetailImagesDto
    {
        public long Id { get; set; }
        public string Src { get; set; }
    }

    public class ProductDetailFeatureDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
    }
}
