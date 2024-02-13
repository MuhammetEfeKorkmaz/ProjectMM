using FullSharedCore.Aspects.Secured.Jwt.Models;

namespace Shared.Aspects.Secured.Jwt
{
    public interface ITokenHelper
    {
        string CreateToken(UserInfo userInfo, List<string> operationClaimsInfo);
    }
}
