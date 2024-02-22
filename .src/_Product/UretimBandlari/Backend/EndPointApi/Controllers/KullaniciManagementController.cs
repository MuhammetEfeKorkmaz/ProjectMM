using Business.Abstract.ForUser;
using DTOs.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace EndPointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class KullaniciManagementController : ControllerBase
    {

        private readonly IKullaniciManagement managemet;
        public KullaniciManagementController(IKullaniciManagement _managemet)
        {
            managemet = _managemet;
        }


        /*
        [HttpPost(Name = "KayitOl")]
        public async Task<IActionResult> KayitOl(SystemUserAddUpdateDto _systemUserDto, CancellationToken _token)
        {
            var result = await managemet.KayitOl(_systemUserDto, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }




        [HttpPost(Name = "GirisYap")]
        public async Task<IActionResult> GirisYap(UserForLoginDto _userForLoginDto, CancellationToken _token)
        {
            var result = await managemet.GirisYap(_userForLoginDto, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }




        [HttpPost(Name = "GirisYapmisKullaniciyaTokenTuret")]
        public async Task<IActionResult> GirisYapmisKullaniciyaTokenTuret(UserForLoginDto _userForLoginDto, CancellationToken _token)
        {
            var result = await managemet.GirisYapmisKullaniciyaTokenTuret(_userForLoginDto, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }




        [HttpPost(Name = "SifremiUnuttum")]
        public async Task<IActionResult> SifremiUnuttum(string _param, CancellationToken _token)
        {
            var result = await managemet.SifremiUnuttum(_param, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }




        [HttpPost(Name = "YetkiEkle")]
        public async Task<IActionResult> YetkiEkle(OperationClaimAddUpdateForSystemUserDto _param, CancellationToken _token)
        {
            var result = await managemet.YetkiEkle(_param, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }




        [HttpPost(Name = "YetkiSil")]
        public async Task<IActionResult> YetkiSil(OperationClaimAddUpdateForSystemUserDto _param, CancellationToken _token)
        {
            var result = await managemet.YetkiSil(_param, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }




        [HttpPost(Name = "YetkileriGetir")]
        public async Task<IActionResult> YetkileriGetir(int _param, CancellationToken _token)
        {
            var result = await managemet.YetkileriGetir(_param, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        */

    }
}
