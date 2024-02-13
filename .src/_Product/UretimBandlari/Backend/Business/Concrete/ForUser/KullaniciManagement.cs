using AutoMapper;
using Business.Abstract;
using Business.ValidationRules.ForUser;
using Dal.Abstract;
using DTOs.UserModels;
using Entities.DbModels.UserModels;
using FullSharedCore.Aspects.Secured;
using FullSharedCore.Aspects.Secured.Hashing;
using FullSharedCore.Aspects.Secured.Jwt.Models;
using FullSharedCore.Aspects.Transaction;
using FullSharedCore.Aspects.Validation;
using FullSharedCore.Utilities.Constants.Message;
using FullSharedCore.Utilities.EmailSender.Abstract;
using FullSharedCore.Utilities.EmailSender.Models;
using FullSharedResults.Results;
using Microsoft.EntityFrameworkCore;
using Shared.Aspects.Secured.Jwt;
namespace Business.Concrete.ForUser
{
    public class KullaniciManagement : IKullaniciManagement
    {
        private IUnitOfWork uow = null;
        private IMailSender mailSender = null;
        private ITokenHelper tokenHelper = null;
        private readonly IMapper mapper;
        public KullaniciManagement(IUnitOfWork _uow, ITokenHelper _tokenHelper , IMailSender _mailSender, IMapper _mapper)
        { 
            uow = _uow;
            mailSender = _mailSender;
            tokenHelper = _tokenHelper;
            mapper = _mapper;
        }






        [ValidationAspect(typeof(RegisterValidator), ValidatorMethodType.Add,Priority =1)]
        [TransactionAspect(Priority =2)]
        public async Task<IDataResult<SystemUserReturnDto>> KayitOl(SystemUserAddUpdateDto systemUserDto, CancellationToken token)
        {

            var DbUser = await uow.systemUserDal.GetByMail(systemUserDto.Email, token);
            if (DbUser is not null)
                return new ErrorDataResult<SystemUserReturnDto>(SystemMessages.UserMessages.ForUser.HaliHazirdaBumailAdresiBirKullaniciyaKayitli);


            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(systemUserDto.Password, out passwordHash, out passwordSalt);

            List<OperationClaims> claimList = new List<OperationClaims>();
            foreach (var item in systemUserDto.OperationClaimsID)
            {
                var dbClaim = await uow.operationClaimsDal.GetById(item, token);
                if (dbClaim is null)
                    throw new Exception(SystemMessages.UserMessages.ForUser.YetkiBulunamadi);

                claimList.Add(dbClaim);
            }




            var user=  mapper.Map<SystemUser>(systemUserDto);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.IsActive = true;
            user.Password = string.Empty;
            user.OperationClaimss.AddRange(claimList);
            await uow.systemUserDal.Add(user, token);
            
             

           


            var userReturn = mapper.Map<SystemUserReturnDto>(user);

            return new SuccessDataResult<SystemUserReturnDto>(userReturn, SystemMessages.UserMessages.ForUser.KullaniciKaydiBasarili);
        }








        [ValidationAspect(typeof(LoginValidator), Priority = 1)]
        [TransactionAspect(Priority = 2)]
        public async Task<IDataResult<SystemUserReturnDto>> GirisYap(UserForLoginDto userForLoginDto, CancellationToken token)
        {
           
            var DbUser = await uow.systemUserDal.GetByMail(userForLoginDto.Email, token);
            if (DbUser is null)
                return new ErrorDataResult<SystemUserReturnDto>(SystemMessages.UserMessages.ForUser.KullaniciBulunamadi);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, DbUser.PasswordHash, DbUser.PasswordSalt))
                return new ErrorDataResult<SystemUserReturnDto>(SystemMessages.UserMessages.ForUser.SifreHatasi);

            var userReturn = mapper.Map<SystemUserReturnDto>(DbUser);

            return new SuccessDataResult<SystemUserReturnDto>(userReturn, SystemMessages.UserMessages.ForUser.KullaniciGirisiBasarili);

        }





        [ValidationAspect(typeof(LoginValidator), Priority = 1)]
        public async Task<IDataResult<string>> GirisYapmisKullaniciyaTokenTuret(UserForLoginDto userForLoginDto, CancellationToken token)
        {
            var DbUser = await uow.systemUserDal.GetByMail(userForLoginDto.Email, token);
            if (DbUser is null)
                return new ErrorDataResult<string>(SystemMessages.UserMessages.ForUser.KullaniciBulunamadi);

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, DbUser.PasswordHash, DbUser.PasswordSalt))
                return new ErrorDataResult<string>(SystemMessages.UserMessages.ForUser.SifreHatasi);



            IList<OperationClaims> claims = DbUser.OperationClaimss;

             

            var accessToken = tokenHelper.CreateToken(new UserInfo() { Email = DbUser.Email, Name = DbUser.Name, Nick = DbUser.Nick, Id= DbUser.Id.ToString() },
                claims.Select(x => x.Name).ToList());

            if (string.IsNullOrEmpty(accessToken))
                return new ErrorDataResult<string>(SystemMessages.UserMessages.ForUser.KullaniciyaAitYetkiBulunamadi);

           
            return new SuccessDataResult<string>(accessToken, SystemMessages.UserMessages.ForPublic.IslemBasarili);

        }






        public async Task<IResult> SifremiUnuttum(string _param, CancellationToken token)
        {
            var DbUser = await uow.systemUserDal.GetByMail(_param, token);
            if (DbUser is null)
                return new ErrorResult(SystemMessages.UserMessages.ForUser.KullaniciBulunamadi);

            string mailSenderResult = mailSender.Sender(new MailSenderModel()
            {
                MailTipi = 3,
                Body = "Şifreniz:  " + DbUser.Password,
                Subject = "Sersim XX Uygulaması, Şifremi Unuttum."
            });

            return string.IsNullOrEmpty(mailSenderResult)
                   ? new SuccessResult(SystemMessages.UserMessages.ForPublic.IslemBasarili)
                   : new ErrorResult(SystemMessages.UserMessages.ForUser.SifremiUnuttumMailiGondermeIslemiBasarisiz + $" Ek Bilgi: {mailSenderResult}");
        }






        //[SecuredOperationAspect("YetkiEkle", Priority = 1)]
        [ValidationAspect(typeof(ClaimsForUserValidator), Priority = 2)]
        [TransactionAspect(Priority =3)]
        public async Task<IResult> YetkiEkle(OperationClaimAddUpdateForSystemUserDto _param, CancellationToken token)
        {
            var DbUser = await uow.systemUserDal.GetById(_param.SystemUserFID, token);
            if (DbUser is null)
                return new ErrorResult(SystemMessages.UserMessages.ForUser.KullaniciBulunamadi);


            var DbClaim = await uow.operationClaimsDal.GetById(_param.OperationClaimsFID, token);
            if (DbClaim is null)
                return new ErrorResult(SystemMessages.UserMessages.ForUser.YetkiBulunamadi);


            //await uow.systemUserOperationClaimsDal.Add(new SystemUserOperationClaims()
            //{
            //    IsActive = true,
            //    OperationClaimsFID = DbClaim.RecId,
            //    SystemUserFID = DbUser.RecId,
            //    RecId = Guid.NewGuid()
            //}, token);


            return new SuccessResult(SystemMessages.UserMessages.ForPublic.IslemBasarili);
        }







        [SecuredOperationAspect("YetkiEkle", Priority = 1)] 
        [ValidationAspect(typeof(ClaimsForUserValidator),ValidatorMethodType.Delete,Priority =2)]
        public async Task<IResult> YetkiSil(OperationClaimAddUpdateForSystemUserDto _param, CancellationToken token)
        {
            var DbUser = await uow.systemUserDal.GetById(_param.SystemUserFID, token);
            if (DbUser is null)
                return new ErrorResult(SystemMessages.UserMessages.ForUser.KullaniciBulunamadi);


            var DbClaim = await uow.operationClaimsDal.GetById(_param.OperationClaimsFID, token);
            if (DbClaim is null)
                return new ErrorResult(SystemMessages.UserMessages.ForUser.YetkiBulunamadi);

           

            // await uow.systemUserOperationClaimsDal.Delete(_param.Id, token);

            
            return new SuccessResult(SystemMessages.UserMessages.ForPublic.IslemBasarili);


        }







        [SecuredOperationAspect("YetkileriGetir", Priority =1)]
        [TransactionAspect(Priority = 2)]
        public async Task<IDataResult<IList<OperationClaims>>> YetkileriGetir(int _UserId, CancellationToken token)
        {
            IList<OperationClaims> result = new List<OperationClaims>();
            var DbUser = await uow.systemUserDal.GetById(_UserId, token);
            if (DbUser is null)
                return new ErrorDataResult<IList<OperationClaims>>(SystemMessages.UserMessages.ForUser.KullaniciBulunamadi);

            //uow.context.SystemUser.Include(x=>x.SystemUserOperationClaimss)
            //    .ThenInclude(x=>x.OperationClaims)


            //var DbUserOperations = await uow.systemUserOperationClaimsDal.Where(x => x.SystemUserFID.Equals(DbUser.RecId)).ToListAsync(token);
            //if (DbUserOperations is null)
            //    return new ErrorDataResult<IList<OperationClaims>>(SystemMessages.UserMessages.ForUser.KullaniciyaAitYetkiBulunamadi);

            //foreach (var item in DbUserOperations)
            //{
            //    result.Add(await uow.operationClaimsDal.GetById(item.Id, token));
            //}

            return new SuccessDataResult<IList<OperationClaims>>(result, SystemMessages.UserMessages.ForPublic.IslemBasarili);

        }










        [SecuredOperationAspect("YetkiKartiEkle", Priority = 1)]
        [TransactionAspect(Priority = 2)]
        public async Task<IResult> YetkiKartiEkle(string _param, CancellationToken token)
        {
            var dbClaim = await uow.operationClaimsDal.Where(x => x.Name.Equals(_param)).FirstOrDefaultAsync(token);
            if (dbClaim is not null)
                return new ErrorResult(SystemMessages.UserMessages.ForUser.YetkiKartiZatenMevcut);


            // await uow.operationClaimsDal.Add(new OperationClaims() { IsActive = true, Name = _param, RecId = Guid.NewGuid() }, token);


            return new SuccessResult(SystemMessages.UserMessages.ForPublic.IslemBasarili);
        }





    }
}
