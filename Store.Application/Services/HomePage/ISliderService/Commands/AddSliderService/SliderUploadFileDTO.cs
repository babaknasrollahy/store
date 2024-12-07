using Microsoft.AspNetCore.Http;

namespace Store.Application.Services.HomePage.ISliderService.Commands.AddSliderService
{
    public class SliderUploadFileDTO
    {
        public IFormFile file { get; set; }
        public string Link { get; set; }
    }
}
