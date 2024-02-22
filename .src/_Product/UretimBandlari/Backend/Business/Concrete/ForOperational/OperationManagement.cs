using AutoMapper;
using Business.Abstract.ForOperational;
using Dal.Abstract.Contexts;
using DTOs.ForOperational;
using DTOs.ProductModels;
using Entities.DbModels.ProductModels;
using FullSharedResults.Results;
using System.Text;

namespace Business.Concrete.ForOperational
{
    public class OperationManagement : IOperationManagement
    {
        private IUnitOfWorkCommand uow = null;
        private readonly IMapper mapper;
        public OperationManagement(IUnitOfWorkCommand _uow, IMapper _mapper)
        {
            uow = _uow;
            mapper = _mapper;
        }



        public async Task<IDataResult<DtoNormalIstasyon>> TestIcinIstasyonGetir(string urunSeri, string istasyonId, CancellationToken token)
        {
            return await Task.FromResult(new SuccessDataResult<DtoNormalIstasyon>(fakeDataNormal(urunSeri)));
        }

        public async Task<IDataResult<DtoKararIstasyonu>> TestIcinKararIstasyonGetir(string urunSeri, string istasyonId, CancellationToken token)
        {
            var fakeData_ = fakeDataKarar(urunSeri); 
            return await Task.FromResult(new SuccessDataResult<DtoKararIstasyonu>(fakeData_));
        }

        public async Task<IDataResult<List<DtoNormalIstasyonSonucOzeti>>> TestIcinIstasyonuYukle(DtoNormalIstasyon dtoNormalIstasyon, CancellationToken token)
        {
            var sonuc = TestIcinIstasyonuYukle_helper(dtoNormalIstasyon);

            //var testUrunSeriBazinda = new TestUrunSeriBazinda();
            //testUrunSeriBazinda.TestGroupKey = dtoNormalIstasyon.Test_GroupKey;
            //testUrunSeriBazinda.UrunSeri = dtoNormalIstasyon.UrunSeri_Seri; 
            //uow.testUrunSeriBazindaDal.Add(testUrunSeriBazinda);

            return await Task.FromResult(new DataResult<List<DtoNormalIstasyonSonucOzeti>>(sonuc, true));
        }
        private List<DtoNormalIstasyonSonucOzeti> TestIcinIstasyonuYukle_helper(DtoNormalIstasyon dtoNormalIstasyon)
        {
            List<DtoNormalIstasyonSonucOzeti> hatalarListesi = new List<DtoNormalIstasyonSonucOzeti>();
            try
            {


                foreach (var Soru in dtoNormalIstasyon.DtoNormalIstasyonDetays.Where(x => x.SablonDetay_SoruTipi == 1).GroupBy(x => x.SablonDetay_GroupKey))
                {
                    DtoNormalIstasyonSonucOzeti nesne = new DtoNormalIstasyonSonucOzeti();
                    foreach (var secenek in Soru)
                    {
                        if (secenek.TestCevap_CevapBool)
                        { // burası bir olumsuzluk ibaresidir.
                            nesne.Soru = Soru.FirstOrDefault().SablonDetay_Soru;
                            if (secenek.SablonDetay_HataDerecesi)
                                nesne.MajorHataliDurumlar.Add(secenek.SablonDetay_Secenek);
                            else
                                nesne.MinorHataliDurumlar.Add(secenek.SablonDetay_Secenek);
                        }
                    }
                    if (!string.IsNullOrEmpty(nesne.Soru))
                        hatalarListesi.Add(nesne);
                }



                foreach (var Soru in dtoNormalIstasyon.DtoNormalIstasyonDetays.Where(x => x.SablonDetay_SoruTipi == 2).GroupBy(x => x.SablonDetay_GroupKey))
                {
                    DtoNormalIstasyonSonucOzeti nesne = new DtoNormalIstasyonSonucOzeti();
                    var secenek = Soru.FirstOrDefault(x => x.TestCevap_CevapBool);
                    if (secenek != null)
                    {
                        nesne.Soru = Soru.FirstOrDefault().SablonDetay_Soru;
                        if (secenek.SablonDetay_HataDerecesi)
                            nesne.MajorHataliDurumlar.Add(secenek.SablonDetay_Secenek);
                        else
                            nesne.MinorHataliDurumlar.Add(secenek.SablonDetay_Secenek);

                    }
                    nesne.DigeriSecilmisHataliDurumlarVarmi = Soru.FirstOrDefault().TestCevap_CevapExtra;

                    if (nesne.DigeriSecilmisHataliDurumlarVarmi)
                    {
                        hatalarListesi.Add(nesne);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(nesne.Soru))
                            hatalarListesi.Add(nesne);
                    }
                }


                foreach (var Soru in dtoNormalIstasyon.DtoNormalIstasyonDetays.Where(x => x.SablonDetay_SoruTipi == 3).GroupBy(x => x.SablonDetay_GroupKey))
                {
                    DtoNormalIstasyonSonucOzeti nesne = new DtoNormalIstasyonSonucOzeti();
                    var soru_ = Soru.FirstOrDefault();
                    if (soru_ != null)
                    {
                        nesne.Soru = soru_.SablonDetay_Soru;
                        if (soru_.TestCevap_CevapBool)
                        {
                            if (soru_.SablonDetay_HataDerecesi)
                                nesne.MajorHataliDurumlar.Add("Evet Seçilmiş");
                            else
                                nesne.MinorHataliDurumlar.Add("Evet Seçilmiş");

                            hatalarListesi.Add(nesne);
                        }

                    }

                }



                foreach (var Soru in dtoNormalIstasyon.DtoNormalIstasyonDetays.Where(x => x.SablonDetay_SoruTipi == 4).GroupBy(x => x.SablonDetay_GroupKey))
                {
                    DtoNormalIstasyonSonucOzeti nesne = new DtoNormalIstasyonSonucOzeti();
                    var soru_ = Soru.FirstOrDefault();
                    if (soru_ != null)
                    {
                        nesne.Soru = soru_.SablonDetay_Soru;
                        if (soru_.TestCevap_CevapBool)
                        {
                            if (soru_.SablonDetay_HataDerecesi)
                                nesne.MajorHataliDurumlar.Add("Var Seçilmiş");
                            else
                                nesne.MinorHataliDurumlar.Add("Var Seçilmiş");

                            hatalarListesi.Add(nesne);
                        }

                    }

                }


                foreach (var Soru in dtoNormalIstasyon.DtoNormalIstasyonDetays.Where(x => x.SablonDetay_SoruTipi == 5).GroupBy(x => x.SablonDetay_GroupKey))
                {
                    int girilenRakam = Convert.ToInt32(Soru.FirstOrDefault().TestCevap_CevapString.Trim());
                    StringBuilder aciklama = new StringBuilder();
                    aciklama.Append($"Girilen Rakam {girilenRakam}, ");

                    DtoNormalIstasyonSonucOzeti nesne = new DtoNormalIstasyonSonucOzeti();
                    foreach (var Durum in Soru)
                    {
                        bool sonucDogru = false;

                        int rakam = Convert.ToInt32(Durum.SablonDetay_Secenek.Split('_')[1]);
                        string case_ = Durum.SablonDetay_Secenek.Split('_')[0];


                        if (case_.EndsWith("x"))
                        {
                            case_ = case_.Remove(1, 1);
                        }
                        if (case_ == ">")
                        {
                            if (girilenRakam > rakam)
                                sonucDogru = true;
                            else
                            {
                                aciklama.Append($"{rakam} Rakamından Büyük, ");
                            }

                        }
                        else if (case_ == ">=")
                        {
                            if (girilenRakam >= rakam)
                                sonucDogru = true;
                            else
                            {
                                aciklama.Append($"{rakam} Rakamından Büyük veya Eşit, ");
                            }
                        }
                        else if (case_ == "<")
                        {
                            if (girilenRakam < rakam)
                                sonucDogru = true;
                            else
                            {
                                aciklama.Append($"{rakam} Rakamından Küçük, ");
                            }
                        }
                        else if (case_ == "<=")
                        {
                            if (girilenRakam <= rakam)
                                sonucDogru = true;
                            else
                            {
                                aciklama.Append($"{rakam} Rakamından Küçük veya Eşit, ");
                            }
                        }
                        else if (case_ == "!=")
                        {
                            if (girilenRakam != rakam)
                                sonucDogru = true;
                            else
                            {
                                aciklama.Append($"{rakam} Rakamına Eşit Olmamalı, ");
                            }
                        }


                    }

                    if (aciklama.Length != 15 + girilenRakam.ToString().Length)
                    {
                        nesne.Soru = Soru.FirstOrDefault().SablonDetay_Soru;
                        if (Soru.FirstOrDefault().SablonDetay_HataDerecesi)
                            nesne.MajorHataliDurumlar.Add($"{aciklama}  Sonuç Yanlış.");
                        else
                            nesne.MajorHataliDurumlar.Add($"{aciklama}  Sonuç Yanlış.");

                        hatalarListesi.Add(nesne);
                    }





                }


                foreach (var Soru in dtoNormalIstasyon.DtoNormalIstasyonDetays.Where(x => x.SablonDetay_SoruTipi == 6).GroupBy(x => x.SablonDetay_GroupKey))
                {
                    DtoNormalIstasyonSonucOzeti nesne = new DtoNormalIstasyonSonucOzeti();
                    var soru_ = Soru.FirstOrDefault();
                    if (soru_ != null)
                    {
                        string dogruCevap = soru_.SablonDetay_Secenek.Split('_')[1];
                        if (soru_.TestCevap_CevapString != dogruCevap)
                        {
                            nesne.Soru = soru_.SablonDetay_Soru;

                            if (soru_.SablonDetay_HataDerecesi)
                                nesne.MajorHataliDurumlar.Add($"{soru_.TestCevap_CevapString} = {dogruCevap}  Sonuç Yanlış.");
                            else
                                nesne.MinorHataliDurumlar.Add($"{soru_.TestCevap_CevapString} = {dogruCevap}  Sonuç Yanlış.");

                            hatalarListesi.Add(nesne);
                        }

                    }
                }




                bool testiGecti = true; int soruBazliMinorHataSayisi = 0;
                foreach (var soru in hatalarListesi)
                {
                    if (soru.MajorHataliDurumlar.Count > 0)
                    {
                        testiGecti = false; break;
                    }
                    soruBazliMinorHataSayisi += soru.MinorHataliDurumlar.Count;
                }

                if (dtoNormalIstasyon.Sablon_KalmasiIcinMinMinorSayisi < soruBazliMinorHataSayisi)
                {
                    testiGecti = false;
                }

                if (hatalarListesi.Count > 0)
                {
                    if (!string.IsNullOrEmpty(hatalarListesi.FirstOrDefault().Soru))
                    {
                        hatalarListesi.FirstOrDefault().Sonuc = testiGecti;
                    }
                    else
                    {
                        hatalarListesi.FirstOrDefault().Sonuc = testiGecti;
                    }
                }
                else
                {
                    hatalarListesi = new List<DtoNormalIstasyonSonucOzeti>();
                    hatalarListesi.FirstOrDefault().Sonuc = testiGecti;
                }

            }
            catch (Exception ex)
            {


            }

            return hatalarListesi;



        }






        private DtoNormalIstasyon fakeDataNormal(string urunSeri)
        {
            var fakeData = new DtoNormalIstasyon();
            fakeData.Sablon_OperatorUyarisi = "Şablondan Gelen İstasyon İçin Uyarı Mesajıdır.";
            fakeData.SablonDetay_Id = 1;
            fakeData.Sablon_Id = 1;
            fakeData.Sablon_KalmasiIcinMinMinorSayisi = 10;
            fakeData.Id = 1;
            fakeData.Bant_Adi = "Bant Adı";
            fakeData.Istasyon_Adi = "İstasyon Adı Demo";
            fakeData.SystemUser_AdiSoyadi = "Kullanıcı Adı";
            fakeData.Test_GroupKey = "qwe123";
            fakeData.Test_Id = 1;
            fakeData.UretimYeri_Adi = "Üretim Yeri";
            fakeData.UrunSeri_Seri = urunSeri;
            fakeData.UrunSeri_Urun = "Ürün Adı";
            fakeData.DtoNormalIstasyonDetays = new List<DtoNormalIstasyonDetay>() {
                
                //soru tipi 1      soru 1       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="a1",
                   SablonDetay_SoruTipi=1,
                   SablonDetay_Soru="Çoktan Seçmeli ve Çok İşaretlemeli Soru Örneği 1",
                   SablonDetay_Secenek="Cevap 1",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="a1",
                   SablonDetay_SoruTipi=1,
                   SablonDetay_Secenek="Cevap 2",
                   SablonDetay_HataDerecesi=true,
              },
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="a1",
                   SablonDetay_SoruTipi=1,
                   SablonDetay_Secenek="Cevap 3",
                   SablonDetay_HataDerecesi=true,
              },

               //soru tipi 1      soru 2       
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="a2",
                   SablonDetay_SoruTipi=1,
                   SablonDetay_Soru="Çoktan Seçmeli ve Çok İşaretlemeli Soru Örneği 2",
                   SablonDetay_Secenek="Cevap 1",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="a2",
                   SablonDetay_SoruTipi=1,
                   SablonDetay_Secenek="Cevap 2",
                   SablonDetay_HataDerecesi=true,
                   SablonDetay_ExtraVeAciklamasi=2,
              },
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="a2",
                   SablonDetay_SoruTipi=1,
                   SablonDetay_Secenek="Cevap 3",
                   SablonDetay_HataDerecesi=false,
              },
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="a2",
                   SablonDetay_SoruTipi=1,
                   SablonDetay_Secenek="Cevap 4",
                   SablonDetay_HataDerecesi=true,
              },






                //soru tipi 2      soru 1       ExtraVeAciklamasi=2 (Exta Alanı Seçilirse Açıklama Zorunlu)
              new DtoNormalIstasyonDetay()
              {
                   SablonDetay_GroupKey="b1",
                   SablonDetay_SoruTipi=2,
                   SablonDetay_Soru="Çoktan Seçmeli ve Tek İşaretlemeli Soru Örneği 1",
                   SablonDetay_Secenek="Cevap 1",
                   SablonDetay_HataDerecesi=true,
                   SablonDetay_ExtraVeAciklamasi=2,
              },
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="b1",
                   SablonDetay_SoruTipi=2,
                   SablonDetay_Secenek="Cevap 2",
                   SablonDetay_HataDerecesi=false,
              },

               //soru tipi 2      soru 2       ExtraVeAciklamasi=3 (Exta Alanı Seçilirse Açıklama Zorunlu Değil)
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="b2",
                   SablonDetay_SoruTipi=2,
                   SablonDetay_Soru="Çoktan Seçmeli ve Tek İşaretlemeli Soru Örneği 2",
                   SablonDetay_Secenek="Cevap 1",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=3,
              },
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="b2",
                   SablonDetay_SoruTipi=2,
                   SablonDetay_Secenek="Cevap 2",
                   SablonDetay_HataDerecesi=true,
              },
              new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="b2",
                   SablonDetay_SoruTipi=2,
                   SablonDetay_Secenek="Cevap 3",
                   SablonDetay_HataDerecesi=false,
              },




                //soru tipi 3      soru 1       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
               new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="c1",
                   SablonDetay_SoruTipi=3,
                   SablonDetay_Soru="Evet/Hayır Soru Örneği 1",
                   SablonDetay_HataDerecesi=true,
                   SablonDetay_ExtraVeAciklamasi=1,
              },



               /*
                //soru tipi 3      soru 2       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="c2",
                   SablonDetay_SoruTipi=3,
                    SablonDetay_Soru="Evet/Hayır Soru Örneği 2",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },
                
                //soru tipi 3      soru 3       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="c3",
                   SablonDetay_SoruTipi=3,
                    SablonDetay_Soru="Evet/Hayır Soru Örneği 3",
                   SablonDetay_HataDerecesi=true,
                   SablonDetay_ExtraVeAciklamasi=1,
              },
                */





                //soru tipi 4      soru 1       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="d1",
                   SablonDetay_SoruTipi=4,
                    SablonDetay_Soru="Var/Yok Soru Örneği 1",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },

                 //soru tipi 4      soru 2       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="d2",
                   SablonDetay_SoruTipi=4,
                    SablonDetay_Soru="Var/Yok Soru Örneği 2",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },
                
                /*
                 //soru tipi 4      soru 3       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="d3",
                   SablonDetay_SoruTipi=4,
                    SablonDetay_Soru="Var/Yok Soru Örneği 3",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },
                
                 //soru tipi 4      soru 4       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="d4",
                   SablonDetay_SoruTipi=4,
                    SablonDetay_Soru="Var/Yok Soru Örneği 4",
                   SablonDetay_HataDerecesi=true,
                   SablonDetay_ExtraVeAciklamasi=1,
              },

                */




                    //soru tipi 5      soru 1       ExtraVeAciklamasi=1 (Exta Alanı Kapalı)  Bu Soru Tipide Kapalıdır.
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="e1",
                   SablonDetay_SoruTipi=5,
                    SablonDetay_Soru="Rakamsal Değer Eşleştirme Örneği 1",
                    SablonDetay_Secenek=">x_20",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },
                      //soru tipi 5      soru 1 
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="e1",
                   SablonDetay_SoruTipi=5,
                      SablonDetay_Secenek="<x_50",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },
                       //soru tipi 5      soru 1 
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="e1",
                   SablonDetay_SoruTipi=5,
                      SablonDetay_Secenek="!=_30",
                   SablonDetay_HataDerecesi=false,
                   SablonDetay_ExtraVeAciklamasi=1,
              },




                        //soru tipi 6      soru 1 
                new DtoNormalIstasyonDetay()
              {
                  SablonDetay_GroupKey="f1",
                   SablonDetay_SoruTipi=6,
                      SablonDetay_Soru="Metinsel Değer Eşleştirme Örneği 1",
                      SablonDetay_Secenek="==_aaa",
                   SablonDetay_HataDerecesi=true,
                   SablonDetay_ExtraVeAciklamasi=1,
              }






            };

            return fakeData;
        }

        private DtoKararIstasyonu fakeDataKarar(string urunSeri)
        {
            var fakeData = new DtoKararIstasyonu();
            fakeData.Sablon_KararIstasyonuUyari = "Şablondan Gelen İstasyon İçin Uyarı Mesajıdır.";
            fakeData.Id = 1;
            fakeData.Bant_Adi = "Bant Adı";
            fakeData.Istasyon_Adi = "İstasyon Adı Demo";
            fakeData.SystemUser_AdiSoyadi = "Kullanıcı Adı";
            fakeData.Test_GroupKey = "qwe123";
            fakeData.UrunSeri_Seri = urunSeri;
            fakeData.Test_Urun = "Ürün Adı";
            fakeData.DtoKararIstasyonuDetays = new List<DtoKararIstasyonuDetay>() {
                
               
              new DtoKararIstasyonuDetay()
              {
                    SystemUser_AdiSoyadi="Personel Bilgisi 1",
                     TestUrunSeriBazinda_Sonuc=true,
                      TestUrunSeriBazinda_TestId=1,
                       TestUrunSeriBazinda_TestTarihi=DateTime.Now
              },
              new DtoKararIstasyonuDetay()
              {
                  SystemUser_AdiSoyadi="Personel Bilgisi 2",
                     TestUrunSeriBazinda_Sonuc=false,
                      TestUrunSeriBazinda_TestId=2,
                       TestUrunSeriBazinda_TestTarihi=DateTime.Now
              },
              new DtoKararIstasyonuDetay()
              {
                  SystemUser_AdiSoyadi="Personel Bilgisi 3",
                     TestUrunSeriBazinda_Sonuc=false,
                      TestUrunSeriBazinda_TestId=3,
                       TestUrunSeriBazinda_TestTarihi=DateTime.Now
              }, 
              new DtoKararIstasyonuDetay()
              {
                  SystemUser_AdiSoyadi="Personel Bilgisi 4",
                     TestUrunSeriBazinda_Sonuc=true,
                      TestUrunSeriBazinda_TestId=4,
                       TestUrunSeriBazinda_TestTarihi=DateTime.Now
              },
              new DtoKararIstasyonuDetay()
              {
                  SystemUser_AdiSoyadi="Personel Bilgisi 5",
                     TestUrunSeriBazinda_Sonuc=true,
                      TestUrunSeriBazinda_TestId=5,
                       TestUrunSeriBazinda_TestTarihi=DateTime.Now
              }, 
            };

            return fakeData;
        }




    }
}
