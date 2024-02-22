using AutoMapper;
using Dal.Abstract.Contexts;
using Dal.Abstract.ForOperational;
using Dal.Abstract.ForUser;
using Dal.Concrete.ForUser;
using Entities.DbModels.UserModels;
using FullSharedCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dal.Concrete.Contexts
{

    public class UnitOfWorkCommand : IUnitOfWorkCommand, IDisposable
    {
        public IMapper mapper { get; }
        private PDbContextCommand Context;
        public UnitOfWorkCommand(PDbContextCommand _context, IMapper _mapper)
        {
            Context = _context;
            mapper = _mapper;
            systemUserDal = new SystemUserDal(Context.Set<SystemUser>());
            operationClaimsDal = new OperationClaimsDal(Context.Set<OperationClaims>());

            ogrenciDal = new OgrenciDal(Context.Set<Ogrenci>());
            sinifDal = new SinifDal(Context.Set<Sinif>());
            adresDal = new AdresDal(Context.Set<Adres>());
            kitapDal = new KitapDal(Context.Set<Kitap>());
            yazarDal = new YazarDal(Context.Set<Yazar>());
        }


        public TDestination Map<TSource, TDestination>(TSource model)
        {
            return mapper.Map<TSource, TDestination>(model);
        }



        public ISystemUserDal systemUserDal { get; private set; }
        public IOperationClaimsDal operationClaimsDal { get; private set; }


        public IOgrenciDal ogrenciDal { get; private set; }
        public ISinifDal sinifDal { get; private set; }
        public IAdresDal adresDal { get; private set; }
        public IKitapDal kitapDal { get; private set; }
        public IYazarDal yazarDal { get; private set; }





        public IBantDal bantDal { get; private set; }
        public IIstasyonDal istasyonDal { get; private set; }
        public ISablonDal sablonDal { get; private set; }
        public ISablonDetayDal sablonDetayDal { get; private set; }
        public ITestCevapDal testCevapDal { get; private set; }
        public ITestDal testDal { get; private set; }
        public ITestUrunBazindaDal testUrunBazindaDal { get; private set; }
        public ITestUrunSeriBazindaDal testUrunSeriBazindaDal { get; private set; }
        public IUretimYeriDal uretimYeriDal { get; private set; }
        public IUrunSeriDal urunSeriDal { get; private set; }





        private CancellationToken? token { get => ServiceRegistiration_FullSharedCore.ServiceProvider.GetService<IHttpContextAccessor>()?.HttpContext?.RequestAborted; }



        public void SaveChanges(Exception e)
        {
            if (token is not null)
            {
                if (!token.Value.IsCancellationRequested)
                {
                    if (e is null)
                        // if (Context.Database.CurrentTransaction is not null)
                        Context.SaveChanges();
                }
            }
            else
            {
                if (e is null)
                    // if (Context.Database.CurrentTransaction is not null)
                    Context.SaveChanges();
            }
        }

        public async Task SaveChangesAsync(Exception e)
        {
            if (token is not null)
            {
                if (!token.Value.IsCancellationRequested)
                {
                    if (e is null)
                        //if (Context.Database is not null)
                        await Context.SaveChangesAsync(token.Value);
                }
            }
            else
            {
                if (e is null)
                    //if (Context.Database.CurrentTransaction is not null)
                    await Context.SaveChangesAsync();
            }
        }



        public async void Dispose()
        {
            if (Context is not null)
            {
                await Context.DisposeAsync();
                Context = null;
            }
        }
    }
}
