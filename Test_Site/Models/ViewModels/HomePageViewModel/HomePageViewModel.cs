using Test_Site_1.Application.Services.Common.Queries.GetHomePageImage;
using Test_Site_1.Application.Services.Common.Queries.GetSlider;
using Test_Site_1.Application.Services.Products.Queries.GetProductForSite;
using Test_Site_1.Domain.Entities.HomePage;

namespace Test_Site.Models.ViewModels.HomePageViewModel
{
    public class HomePageViewModel
    {
        public List<SliderDto> sliderDtos { get; set; }
        public List<HomePageImageDto> PageImages { get; set; }
        public List<ProductForSiteDto> Camera {  get; set; }
        public List<ProductForSiteDto> Mobile { get; set; }
        public List<ProductForSiteDto> Laptop { get; set; }

    }
}
