using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.Products;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Test_Site_1.Application.Services.Products.Commands.AddNewProduct.AddNewProductService;


namespace Test_Site_1.Application.Services.Products.Commands.AddNewProduct
{
    public interface IAddNewProductService
    {
        ResultDto Execute(RequestAddNewProductDto request);
    }



    public class AddNewProductService : IAddNewProductService
    {
        private readonly IDataBaseContext _context;
        private readonly IWebHostEnvironment _environment;
        public AddNewProductService(IDataBaseContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public ResultDto Execute(RequestAddNewProductDto request)
        {
            try
            {
                var productName = request.Name.Replace(' ', '-');

                Product product = new Product()
                {
                    Brand = request.Brand,
                    Name = request.Name,
                    Display = request.Displayed,
                    Inventory = request.Inventory,
                    CategoryId = request.CategoryId,
                    Description = request.Description,
                    Price = request.Price,
                };
                _context.Products.Add(product);

                List<ProductFeatures> features = new List<ProductFeatures>();
                foreach (var item in request.Features)
                {

                    features.Add(new ProductFeatures
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product
                    });
                }
                _context.ProductFeatures.AddRange(features);

                IList<ProductImages> images = new List<ProductImages>();
                foreach (var item in request.Images)
                {
                    var uploadResult = UploadFile(item);
                    images.Add(new ProductImages
                    {
                        Product = product,
                        Src = uploadResult.FileNameAddress
                    });
                }
                
                
                
                _context.ProductImages.AddRange(images);
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "کالا با موفقیت ثبت شد"
                };
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "کالا ثبت نشد"
                };
            }
        }





        private UploadDto UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            string folder = $@"images\ProductImages\";
            var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
            if (!Directory.Exists(uploadsRootFolder))
            {
                Directory.CreateDirectory(uploadsRootFolder);
            }

            string fileName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsRootFolder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return new UploadDto
            {
                FileNameAddress = Path.Combine(folder, fileName),
                Status = true
            };
        }




        internal class UploadDto
        {
            public bool Status { get; set; }
            public string FileNameAddress { get; set; }
        }

        public class RequestAddNewProductDto
        {
            public string Name { get; set; }
            public string Brand { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public int Inventory { get; set; }
            public long CategoryId { get; set; }
            public bool Displayed { get; set; }
            public List<IFormFile> Images { get; set; }
            public List<AddNewProduct_Features> Features { get; set; }
        }

        public class AddNewProduct_Features
        {
            public string DisplayName { get; set; }
            public string Value { get; set; }
        }
    }
}
