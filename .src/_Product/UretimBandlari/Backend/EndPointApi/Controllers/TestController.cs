using Business.Abstract.ForUser;
using DTOs.UserModels.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EndPointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ITestManagement management;
        public TestController(ITestManagement _management)
        {
            management = _management;
        }


        [HttpPost(Name = "AddOgrenci")]
        public async Task<IActionResult> AddOgrenci(CancellationToken _token)
        {
            OgrenciAddCommandDto request = new();
            request.Adi = "Samet 1";
            request.Soyadi = "Koramaz 1";
            request.AdresAdi = "Kocasinan/Kayseri 1";
            request.SinifAdi = "11-A 1";
            request.Kitaps = new List<string>() { "Kitap 1", "Kitap 2", "Kitap 3", "Kitap 4" };

            var result = await management.AddOgrenci(request, _token); 
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }



    }
}
