using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;
using Test_Site_1.Domain.Entities.HomePage;

namespace Test_Site_1.Application.Services.Common.Queries.GetHomePageImage
{
    public interface IGetHomePageImagesService
    {
        ResultDto<List<HomePageImageDto>> Execute();

        
    }
    public class GetHomePageImagesService:IGetHomePageImagesService 
    {
        private readonly IDataBaseContext _context;
        public GetHomePageImagesService(IDataBaseContext context)
        {
            _context = context; 
        }

        public ResultDto<List<HomePageImageDto>> Execute() 
        {
            var image = _context.HomePageImages.OrderByDescending(x => x.Id).Select(x=>new HomePageImageDto 
            {
                Id = x.Id,  
                Link = x.Link,
                Src = x.Src,
                ImageLocation = x.ImageLocation,
            }).ToList();
            return new ResultDto<List<HomePageImageDto>>()
            {
                Data = image,
                IsSuccess = true,

            };
        }
    }
   
   

    public class HomePageImageDto 
    {
        public long Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }    
       
    }
}
