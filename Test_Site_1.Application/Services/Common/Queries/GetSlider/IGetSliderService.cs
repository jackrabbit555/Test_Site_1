using Test_Site_1.Application.Interfaces.Contexts;
using Test_Site_1.Common.Dto;

namespace Test_Site_1.Application.Services.Common.Queries.GetSlider
{
    public interface IGetSliderService
    {
        ResultDto<List<SliderDto>> Execute();


    }

    public class GetSliderDtoService : IGetSliderService
    {


        private readonly IDataBaseContext _context;


        public GetSliderDtoService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<SliderDto>> Execute()
        {
            var sliders = _context.Sliders.OrderByDescending(s => s.Id).ToList().Select(
                s => new SliderDto

                {
                    Link = s.Link,
                    Src = s.Src,


                }).ToList();

            return new ResultDto<List<SliderDto>>()
            {
                Data = sliders,
                IsSuccess = true
            };
        }
    }

    public class SliderDto
    {
        public string Src { get; set; }
        public string Link { get; set; }
    }
}
