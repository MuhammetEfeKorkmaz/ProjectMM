using AutoMapper;
using Business.Abstract.ForUser;
using Business.Concrete.ForUser.Commands.Handlers;
using Business.Concrete.ForUser.Commands.Models;
using Dal.Abstract.Contexts;
using DTOs.UserModels.Commands;
using FullSharedCore.Aspects.Transaction;
using FullSharedCore.Helpers.LoadAssemblyies;
using FullSharedResults.Results;
using MediatR;
using System.Reflection;

namespace Business.Concrete.ForUser
{
    public class TestManagement : ITestManagement
    {
        private readonly IMediator mediator;
       // private readonly IUnitOfWorkCommand uow;
        private readonly IMapper mapper;
        //public TestManagement(IUnitOfWorkCommand _uow, IMapper _mapper, IMediator _mediator)
        //{
        //    mediator = _mediator;
        //    uow = _uow;
        //    mapper = _mapper;
        //}
        public TestManagement(IMapper _mapper, IMediator _mediator)
        {
            mediator = _mediator; 
            mapper = _mapper;
        }



        [TransactionAspectCommandDb(Priority = 1)]
        public async Task<IDataResult<OgrenciAddCommandDto>> AddOgrenci(OgrenciAddCommandDto request, CancellationToken token)
        {
            var privatemodel = mapper.Map<OgrenciAddCommandDto, OgrenciAddCommandMeditr>(request); 
            var result = await mediator.Send(privatemodel, token);

            return result is null
                ? new DataResult<OgrenciAddCommandDto>(null, false)
                : new DataResult<OgrenciAddCommandDto>(privatemodel, true);

        }


    }



}
