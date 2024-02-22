using Business.Concrete.ForUser.Commands.Models;
using DTOs.UserModels.Commands;
using FullSharedResults.Results;

namespace Business.Abstract.ForUser
{
    public interface ITestManagement
    {

        Task<IDataResult<OgrenciAddCommandDto>> AddOgrenci(OgrenciAddCommandDto request, CancellationToken token);


    }
}
