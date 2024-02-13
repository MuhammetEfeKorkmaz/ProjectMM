using Business.ValidationRules.ForUser;
using DTOs.UserModels;
using Entities.DbModels.UserModels;
using FullSharedCore.Aspects.Transaction;
using FullSharedCore.Aspects.Validation;
using FullSharedResults.Results;

namespace Business.Abstract
{
    public interface IKullaniciManagement
    {

      
        public Task<IDataResult<SystemUserReturnDto>> KayitOl(SystemUserAddUpdateDto systemUserDto, CancellationToken token);





         







      
        public Task<IDataResult<SystemUserReturnDto>> GirisYap(UserForLoginDto userForLoginDto, CancellationToken token);









     
        public Task<IDataResult<string>> GirisYapmisKullaniciyaTokenTuret(UserForLoginDto userForLoginDto, CancellationToken token);









        public Task<IResult> SifremiUnuttum(string _param, CancellationToken token);









     
        public Task<IResult> YetkiEkle(OperationClaimAddUpdateForSystemUserDto _param, CancellationToken token);










     
        public Task<IResult> YetkiSil(OperationClaimAddUpdateForSystemUserDto _param, CancellationToken token);








        public Task<IDataResult<IList<OperationClaims>>> YetkileriGetir(int _UserId, CancellationToken token);








     
        public Task<IResult> YetkiKartiEkle(string _param, CancellationToken token);

    }
}
