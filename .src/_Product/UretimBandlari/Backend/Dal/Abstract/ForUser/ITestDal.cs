using Entities.DbModels.UserModels;
using FullSharedCore.DataAccess.Abstract;

namespace Dal.Abstract.ForUser
{
    public interface IOgrenciDal : IRepository<Ogrenci> { }
    public interface ISinifDal : IRepository<Sinif> { }
    public interface IAdresDal : IRepository<Adres> { }
    public interface IKitapDal : IRepository<Kitap> { }
    public interface IYazarDal : IRepository<Yazar> { }
}
