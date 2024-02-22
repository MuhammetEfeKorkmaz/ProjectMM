using AutoMapper;
using Business.Concrete.ForUser.Commands.Models;
using Dal.Abstract.Contexts;
using DTOs.UserModels.Commands;
using Entities.DbModels.UserModels;
using FullSharedCore.Aspects.Transaction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete.ForUser.Commands.Handlers
{

    public class OgrenciAddCommandHandler : IRequestHandler<OgrenciAddCommandMeditr, OgrenciAddCommandDto>
    {
        private readonly IUnitOfWorkCommand uow;
        private readonly IMapper mapper;
        public OgrenciAddCommandHandler(IUnitOfWorkCommand _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }


        [TransactionAspectCommandDb(IsForcedSaveChanges:true, Priority = 1)]
        public async Task<OgrenciAddCommandDto> Handle(OgrenciAddCommandMeditr request, CancellationToken cancellationToken)
        {
            
            var ogrenciDb = await uow.ogrenciDal.Where(x => x.Adi.Equals(request.Adi) & x.Soyadi.Equals(request.Soyadi)).FirstOrDefaultAsync(cancellationToken);//.IncludeMultiple(x => x.Adres, x => x.Sinif).Join(x => x.Kitaps).ThenJoin(x => x.Kitap).ThenJoin(x => x.Yazars).FirstOrDefaultAsync(cancellationToken);
            if (ogrenciDb is not null)
                return new OgrenciAddCommandDto();


            var sinifIdDb = await uow.sinifDal.Where(x => x.SinifAdi.Equals(request.SinifAdi)).Select(x => x.Id).FirstOrDefaultAsync(cancellationToken);
            var kitapIdsDb = await uow.kitapDal.Where(x => request.Kitaps.Contains(x.KitapAdi)).Select(x => x.Id).ToListAsync(cancellationToken);


            Ogrenci ogrenci = new();
            ogrenci.Adi = request.Adi;
            ogrenci.Soyadi = request.Soyadi;


            // Öğrenci Principal entitiy olduğu için, Öğrenci yok ise Adress depency entitiy 'si olamaz. o yüzden doğrudan oluşturulur.
            ogrenci.Adres = new() { AdresAdi = request.AdresAdi };



            if (sinifIdDb != 0)
                ogrenci.SinifId = sinifIdDb;
            else
                ogrenci.Sinif = new() { SinifAdi = request.SinifAdi };



            var crosstable = new HashSet<OgrenciKitap>();
            if (kitapIdsDb is null || kitapIdsDb.Count == 0)
                foreach (var item in request.Kitaps)
                    crosstable.Add(new OgrenciKitap() { Kitap = new Kitap() { KitapAdi = item } });
            else
                foreach (var item in kitapIdsDb)
                    crosstable.Add(new OgrenciKitap() { KitapId = item });

            ogrenci.Kitaps = crosstable;


            await uow.ogrenciDal.AddAsync(ogrenci);

            return request;
        }
    }
}
