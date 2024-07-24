using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

using Test_Site_1.Domain.Entities.Products;
using static Test_Site_1.Application.Services.Products.Commands.EditProduct.EditProductService;

namespace Test_Site_1.Application.Services.Products.Commands.EditProduct
{
    public interface IEditProductService
    {
        ResultDto Execute(RequestEditProductDto request);

    }


    public class EditProductService : IEditProductService
    {
        private readonly IDataBaseContext _context;

        public EditProductService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestEditProductDto request)
        {
            var product = _context.Products.Find(request.ProductId);
            if (product == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "محصول یافت نشد"
                };
            }

            // Update basic product in
            // mation
            product.Name = request.Name;
            product.Brand = request.Brand;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Inventory = request.Inventory;
            product.Display = request.Display;
            product.CategoryId = request.CategoryId;

            // Update images
            UpdateProductImages(product, request.ProductImages);

            // Update features
            UpdateProductFeatures(product, request.ProductFeatures);

            _context.SaveChanges();

            return new ResultDto
            {
                IsSuccess = true,
                Message = "محصول با موفقیت ویرایش شد"
            };
        }

        private void UpdateProductImages(Product product, List<ProductImageDto> productImages)
        {
            // Clear existing images
            product.ProductImages.Clear();

            // Add new images
            if (productImages != null && productImages.Any())
            {
                foreach (var imageDto in productImages)
                {
                    var image = new ProductImages
                    {
                        Src = imageDto.Src,
                        ProductId = product.Id
                    };
                    product.ProductImages.Add(image);
                }
            }
        }

        private void UpdateProductFeatures(Product product, List<ProductFeatureDto> productFeatures)
        {
            // Clear existing features
            product.ProductFeatures.Clear();

            // Add new features
            if (productFeatures != null && productFeatures.Any())
            {
                foreach (var featureDto in productFeatures)
                {
                    var feature = new ProductFeatures
                    {
                        DisplayName = featureDto.DisplayName,
                        Value = featureDto.Value,
                        ProductId = product.Id
                    };
                    product.ProductFeatures.Add(feature);
                }
            }
        }





        public class RequestEditProductDto
        {
            public long ProductId { get; set; }
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public int Inventory { get; set; }
            public bool Display { get; set; }
            public long CategoryId { get; set; }
            public List<ProductImageDto> ProductImages { get; set; }
            public List<ProductFeatureDto> ProductFeatures { get; set; }
        }

    }
}
