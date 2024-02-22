using Dal.Abstract.ForUser;
using Entities.DbModels.UserModels;
using FullSharedCore.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Dal.Concrete.ForUser
{
    public class OgrenciDal : Repository<Ogrenci>, IOgrenciDal { public OgrenciDal(DbSet<Ogrenci> dbset) : base(dbset) { } }
    public class SinifDal : Repository<Sinif>, ISinifDal { public SinifDal(DbSet<Sinif> dbset) : base(dbset) { } }
    public class AdresDal : Repository<Adres>, IAdresDal { public AdresDal(DbSet<Adres> dbset) : base(dbset) { } }
    public class KitapDal : Repository<Kitap>, IKitapDal { public KitapDal(DbSet<Kitap> dbset) : base(dbset) { } }
    public class YazarDal : Repository<Yazar>, IYazarDal { public YazarDal(DbSet<Yazar> dbset) : base(dbset) { } }
     
}
