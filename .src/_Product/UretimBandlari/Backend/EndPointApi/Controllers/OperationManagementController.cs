using Business.Abstract.ForOperational;
using DTOs.ForOperational;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EndPointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OperationManagementController : ControllerBase
    {
        private readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() };
        private readonly IOperationManagement managemet;
        public OperationManagementController(IOperationManagement _managemet)
        {
            managemet = _managemet;
        }


        [HttpGet(Name = "TestIcinIstasyonGetir")]
        public async Task<IActionResult> TestIcinIstasyonGetir(string param, string param2, CancellationToken _token)
        {
            var result = await managemet.TestIcinIstasyonGetir(param, param2, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [HttpPost(Name = "TestIcinIstasyonuYukle")]
        public async Task<IActionResult> TestIcinIstasyonuYukle(DtoNormalIstasyon param, CancellationToken _token)
        {
            var result = await managemet.TestIcinIstasyonuYukle(param, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }




        [HttpGet(Name = "TestIcinKararIstasyonGetir")]
        public async Task<IActionResult> TestIcinKararIstasyonGetir(string param, string param2, CancellationToken _token)
        {
            var result = await managemet.TestIcinKararIstasyonGetir(param, param2, _token);
            if (result.Success)
                return Ok(result);
            else
                return BadRequest(result);
        }



    }

  
}
