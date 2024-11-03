using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.HomePage;
using static Test_Site_1.Application.Services.Products.Commands.AddNewProduct.AddNewProductService;

namespace Test_Site_1.Application.Services.HomePage.AddNewSlider
{
    public interface IAddNewSliderService
    {

        ResultDto Execute(IFormFile file, string Link);
    }


   
    public class AddNewSliderService : IAddNewSliderService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IDataBaseContext _context;

        public AddNewSliderService(IWebHostEnvironment environment , IDataBaseContext context)
        {
            _environment = environment;
            _context = context;
        }
        public ResultDto Execute(IFormFile file,string Link)
        {
           var resultUpload =  UploadFile(file);

            Slider slider = new Slider()
            {
                Link = Link,
                Src = resultUpload.FileNameAddress,

            };
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,

            };

            
            
               
        }
        private UploadDto UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            string folder = $@"images\HomePages\Sliders";
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
    }


  

    internal class UploadDto
    {
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }
}
