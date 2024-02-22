using AutoMapper;
using Dal.Abstract.ForOperational;
using Dal.Abstract.ForUser;
using FullSharedCore.DataAccess.Abstract;

namespace Dal.Abstract.Contexts
{

    public interface IUnitOfWorkQuery : IBaseUnitOfWorkQuery
    {
        public IMapper mapper { get; }
        TDestination Map<TSource, TDestination>(TSource model);


        public ISystemUserDal systemUserDal { get; }
        public IOperationClaimsDal operationClaimsDal { get; }




        public IOgrenciDal ogrenciDal { get; }
        public ISinifDal sinifDal { get; }
        public IAdresDal adresDal { get; }
        public IKitapDal kitapDal { get; }
        public IYazarDal yazarDal { get; }




        public IBantDal bantDal { get; }
        public IIstasyonDal istasyonDal { get; }
        public ISablonDal sablonDal { get; }
        public ISablonDetayDal sablonDetayDal { get; }
        public ITestCevapDal testCevapDal { get; }
        public ITestDal testDal { get; }
        public ITestUrunBazindaDal testUrunBazindaDal { get; }
        public ITestUrunSeriBazindaDal testUrunSeriBazindaDal { get; }
        public IUretimYeriDal uretimYeriDal { get; }
        public IUrunSeriDal urunSeriDal { get; }
    }
}
