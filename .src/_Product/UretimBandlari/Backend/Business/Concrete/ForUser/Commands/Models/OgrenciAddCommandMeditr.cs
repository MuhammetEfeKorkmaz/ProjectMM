using DTOs.UserModels.Commands;
using MediatR;

namespace Business.Concrete.ForUser.Commands.Models
{
    public class OgrenciAddCommandMeditr : OgrenciAddCommandDto, IRequest<OgrenciAddCommandDto> { }
}
