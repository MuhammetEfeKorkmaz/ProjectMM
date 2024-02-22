using AutoMapper;
using Business.Concrete.ForUser.Commands.Models;
using DTOs.UserModels.Commands;
using FullSharedResults.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndPointApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class Test2Controller : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        public Test2Controller(IMediator _mediator, IMapper _mapper)
        {
            mediator= _mediator;
            mapper = _mapper;
        }


        [HttpPost(Name = "AddOgrenci2")]
        public async Task<IActionResult> AddOgrenci2(CancellationToken _token)
        {
            OgrenciAddCommandDto request = new();
            request.Adi = "Samet 1";
            request.Soyadi = "Koramaz 1";
            request.AdresAdi = "Kocasinan/Kayseri 1";
            request.SinifAdi = "11-A 1";
            request.Kitaps = new List<string>() { "Kitap 1", "Kitap 2", "Kitap 3", "Kitap 4" };


            var privatemodel = mapper.Map<OgrenciAddCommandDto, OgrenciAddCommandMeditr>(request);
            var result = await mediator.Send(privatemodel, _token);

            var resultApi= result is null
                ? new DataResult<OgrenciAddCommandDto>(null, false)
                : new DataResult<OgrenciAddCommandDto>(privatemodel, true);

             
            if (resultApi.Success)
                return Ok(resultApi);
            else
                return BadRequest(resultApi);
        }


    }
}
