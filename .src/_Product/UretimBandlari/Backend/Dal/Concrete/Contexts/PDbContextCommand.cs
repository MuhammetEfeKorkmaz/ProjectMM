using Entities.DbModels.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;



namespace Dal.Concrete.Contexts
{

    public class PDbContextCommand : DbContext
    {
        void MyConfiguration()
        {
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
            //ChangeTracker.AutoDetectChangesEnabled = false; 
            // ChangeTracker.CascadeChanges();


        }


        public PDbContextCommand(DbContextOptions<PDbContextCommand> options) : base(options)
        {
            MyConfiguration();
        }
        public PDbContextCommand()
        {
            MyConfiguration();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("Kullanılacak Veri Tabanı Ayarları yapılmamış.");
            }

        }



        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<OperationClaims> OperationClaims { get; set; }



        /*
        public virtual DbSet<Bant> Bant { get; set; } 
        public virtual Istasyon Istasyon { get;   set; }
        public virtual Sablon Sablon { get;   set; }
        public virtual SablonDetay SablonDetay { get;   set; }
        public virtual TestCevap TestCevap { get;   set; }
        public virtual Test Test { get;   set; }
        public virtual TestUrunBazinda TestUrunBazinda { get;   set; }
        public virtual TestUrunSeriBazinda TestUrunSeriBazinda { get;   set; }
        public virtual UretimYeri UretimYeri { get;   set; }
        public virtual UrunSeri UrunSeri { get;   set; }
        */


        public virtual DbSet<Ogrenci> Ogrenci { get; set; }
        public virtual DbSet<Sinif> Sinif { get; set; }
        public virtual DbSet<Adres> Adres { get; set; }
        public virtual DbSet<Kitap> Kitap { get; set; }
        public virtual DbSet<Yazar> Yazar { get; set; }
        public virtual DbSet<OgrenciKitap> OgrenciKitap { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OgrenciKitap>().HasKey(x=>new {x.OgrenciId, x.KitapId,x.Id});
            modelBuilder.Entity<OgrenciKitap>().HasOne(x=>x.Ogrenci).WithMany(x=>x.Kitaps).HasForeignKey(x=>x.OgrenciId);
            modelBuilder.Entity<OgrenciKitap>().HasOne(x=>x.Kitap).WithMany(x=>x.Ogrencis).HasForeignKey(x=>x.KitapId);


            modelBuilder.Entity<YazarKitap>().HasKey(x => new { x.YazarId, x.KitapId,x.Id });
            modelBuilder.Entity<YazarKitap>().HasOne(x => x.Yazar).WithMany(x => x.Kitaps).HasForeignKey(x => x.YazarId);
            modelBuilder.Entity<YazarKitap>().HasOne(x => x.Kitap).WithMany(x => x.Yazars).HasForeignKey(x => x.KitapId);


            modelBuilder.Entity<Ogrenci>().HasOne<Sinif>(s => s.Sinif).WithMany(g => g.Ogrencis).HasForeignKey(s => s.SinifId);

            
            // her adresin bağlı olduğu bir öğrenci olmalı. ama her öğrencinin adres bilgisi henüz girilmemiş olabilir.
            // dependecy entitiy 'Adres' ,  Principal entiticy ise 'Ogrenci' dir.
            modelBuilder.Entity<Adres>().HasOne(x => x.Ogrenci).WithOne(x => x.Adres).HasForeignKey<Adres>(x=>x.Id);
          
        }

    }
}
