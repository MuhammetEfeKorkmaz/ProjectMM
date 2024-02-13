using FluentValidation.Results;

namespace FullSharedCore.Extensions.PacketCustomException.Models
{
    public class ValidationErrorDetails : ErrorDetails
    {
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}
