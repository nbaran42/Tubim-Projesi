using Hangfire;
using MediatR;
using TubimProject.Application.DTOs.Services.DeserializeModels.ResponseModels.Esya;
using TubimProject.Application.DTOs.Services.DeserializeModels.ResponseModels.Olay;
using TubimProject.Application.DTOs.Services.DeserializeModels.ResponseModels.Sahis;
using TubimProject.Application.Enums;
using TubimProject.Application.Features.CrashReport.Command.Create;
using TubimProject.Application.Features.Madde.Command;
using TubimProject.Application.Features.Madde.Command.CreateMaddeCommand;
using TubimProject.Application.Features.Madde.Command.DeleteMadde;
using TubimProject.Application.Features.Madde.Command.UpdateMaddeCommand;
using TubimProject.Application.Features.Madde.Queries.GetSingleMadde;
using TubimProject.Application.Features.Madde.Queries.GetSonMadde;
using TubimProject.Application.Features.Olay.Command;
using TubimProject.Application.Features.Olay.Queries.GetAllOlaylar;
using TubimProject.Application.Features.Olay.Queries.GetSonOlay;
using TubimProject.Application.Features.OlayDetay.Command;
using TubimProject.Application.Interfaces.Job;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayModule;
using TubimProject.Application.Interfaces.WebServices;
using TubimProject.WebServices.Business;

namespace TubimProject.WebServices.Jobs.Implementations
{
    public class JandarmaServiceJob : IRecurringJob
    {
        public string CronExpression => Cron.Minutely();

        public string JobId => "JandarmaServiceJob";

        private IMediator _mediator;
        private IServiceContext _serviceContext;
        private Serilog.ILogger _logger;
        public JandarmaServiceJob(IMediator mediator, IServiceContext serviceContext, Serilog.ILogger logger)
        {
            _mediator=mediator;
            _serviceContext=serviceContext;
            _logger=logger;
        }

        public async Task Execute()
        {

            #region Olay Service Call

            bool restartOlay = false;
            int serviceReCallAttemptCount = 0;
            _logger.Warning("Jandarma Servisi {0} Zamanında Başlatıldı", DateTime.UtcNow);
            try
            {
                var sonOlay = await _mediator.Send(new GetSonOlayQuery(Enum_Kurumlar.JANDARMA));
                var olaylar = await _mediator.Send(new GetAllOlaysQuery());
                var sonOlayId = sonOlay.Data;
                var olayBilgisi = _serviceContext.GetServiceData<JOlayBilgisi>(sonOlayId, JTabloEnums.JKIP_OLAY_SUC_BILGILERI);

                if (olayBilgisi!=null && olayBilgisi.aktarilacaklarListesi.sonucKod==(int)JDurumKodEnums.KAYITYOK)
                    _logger.Warning("*** Jandarma Genel Komutanlığı Olay Servisi Başarıyla Çağrıldı. Ama Yeni Kayıt Gelmedi");




                if (olayBilgisi!=null && olayBilgisi.aktarilacaklarListesi.sonucKod==(int)JDurumKodEnums.KAYITVAR)
                {
                    foreach (var olay in olayBilgisi.aktarilacaklarListesi.sonucList)
                    {
                        long? olayNo = olay.OlayNo;


                        var yeniOlay = await _mediator.Send(new CreateOlayKayitCommand()
                        {
                            Durum=olay.Durum,
                            HedefUlkesi=olay.HedefYeri,
                            KacakcilikYontemi=olay.KacakcilikYontemi,
                            KurumOlayId=olay.Id,
                            KurumRef=(int)Enum_Kurumlar.JANDARMA,
                            OlayNumarasi=olay.OlayNo,
                            OlayTarihi=olay.OlayTarihi
                        });

                        if (yeniOlay.Succeeded)
                        {
                            var olayDetay = await _mediator.Send(new CreateOlayDetayKayitCommand()
                            {
                                OlayIli=olay.Olayili,
                                OlayIlcesi=olay.Olayilcesi,
                                OlayMahalle=olay.OlayMahallesi,
                                OlayId=olay.Id,
                            });
                            if (olayDetay.Failed)
                            {
                                _logger.Error("Jandarma Servisi Çağrılırken Hata Oluştu.Hata Açıklaması: {0}", olayDetay.Message);
                                restartOlay=true;
                                serviceReCallAttemptCount++;
                                if (serviceReCallAttemptCount==5)
                                    restartOlay=false;
                            }
                        }
                        else
                        {
                            _logger.Error("Jandarma Servisi Çağrılırken Hata Oluştu.Hata Açıklaması: {0}", yeniOlay.Message);
                            restartOlay=true;
                            serviceReCallAttemptCount++;
                            if (serviceReCallAttemptCount==5)
                                restartOlay=false;

                        }

                    }
                }

                if (olayBilgisi==null)
                {
                    _logger.Error("Jandarma Servisi Çağrılırken Hata Oluştu.Servis Null Değer Döndürdü.");
                    serviceReCallAttemptCount++;
                    restartOlay=true;
                    if (serviceReCallAttemptCount==5)
                        restartOlay=false;
                }

            }
            catch (Exception ex)
            {
                _logger.Error("Jandarma Servisi Çağrılırken Hata Oluştu.Hata Açıklaması: {0}", ex.Message);
                serviceReCallAttemptCount++;
                restartOlay=true;
                if (serviceReCallAttemptCount==5)
                    restartOlay=false;

            } while (restartOlay) ;
            #endregion


            #region Madde Service
            int maddeHataSayisi = 0;
MaddeService:
            try
            {

                var sonMaddeId = await _mediator.Send(new GetSonMaddeQuery());
                var maddeBilgisi = _serviceContext.GetServiceData<JEsyaBilgisi>(sonMaddeId.Data, JTabloEnums.JKIP_OLAY_MALZEME_TABLOSU);


                if (maddeBilgisi!=null && maddeBilgisi.aktarilacaklarListesi.sonucKod==(int)JDurumKodEnums.KAYITYOK)
                    _logger.Warning("*** Jandarma Genel Komutanlığı Madde Servisi Başarıyla Çağrıldı. Ama Yeni Kayıt Gelmedi");


                if (maddeBilgisi!=null && maddeBilgisi.aktarilacaklarListesi.sonucKod==(int)JDurumKodEnums.KAYITVAR)
                {
                    foreach (var madde in maddeBilgisi.aktarilacaklarListesi.sonucList)
                    {
                        long oNo = madde.OlayNo;
                        string mNo = madde.MalzemeNo.ToString();
                        double miktar = 0;
                        string GruplanmisAnaTuru = string.Empty;


                        if (madde.Miktari>0)
                            miktar=Convert.ToDouble(madde.Miktari);

                        string malzemeAdi = string.Empty;
                        if (!string.IsNullOrEmpty(madde.MalzemeAdi))
                        {
                            malzemeAdi=madde.MalzemeAdi.ToUpper();
                            Utils.MaddeAdiCevirici(malzemeAdi, ref GruplanmisAnaTuru);
                        }

                        var maddeVarmi = await _mediator.Send(new GetSingleMaddeQuery(olayNo: oNo, malzemeNo: mNo));
                        if (maddeVarmi.Succeeded)
                        {
                            if (maddeVarmi.Data!=null)
                            {
                                if (madde.Durum.ToUpper()=="S")
                                {
                                    await _mediator.Send(new DeleteMaddeCommand(maddeVarmi.Data));
                                }
                                else if (madde.Durum.ToUpper()=="D")
                                {
                                    await _mediator.Send(new UpdateMaddeCommand(maddeVarmi.Data, malzemeAdi));
                                }
                            }
                            else
                            {
                                await _mediator.Send(new CreateMaddeCommand
                                {
                                    JandarmaMaddeId=madde.Id,
                                    AnaTuru=GruplanmisAnaTuru,
                                    Birimi=madde.OlcuBirimi,
                                    Miktari=madde.Miktari,
                                    MalzemeNo=madde.MalzemeNo.ToString(),
                                    OlayNumarasi=oNo,
                                    Durum=madde.Durum.ToString().ToUpper(),
                                    ServisAnaTuru=malzemeAdi
                                });
                            }
                        }
                    }
                }


                if (maddeBilgisi.aktarilacaklarListesi.sonucList.Count()==300 || maddeBilgisi.aktarilacaklarListesi.sonucList.Count()>0)
                    goto MaddeService;




                if (maddeBilgisi==null)
                {
                    maddeHataSayisi++;
                    _logger.Warning("*** Jandarma Web Servisinde Hata Meydana Geldi.Servis {0}. Kez Tekrar Deneniyor.Max. Deneme Sayısı 6.", maddeHataSayisi);
                    if (maddeHataSayisi<7)
                        goto MaddeService;

                }
            }
            catch (Exception ex)
            {
                _logger.Error("*** Servis Çağrımı ya da Veritabanına Yazılırken Hata Meydana Geldi.Hata Bilgisi {0}", ex.ToString());
            }



            #endregion



            #region Şüpheli Service
//            var Supheliler = _serviceContext.GetServiceData<JSahisBilgisi>(sonMaddeId.Data, JTabloEnums.JKIP_OLAY_MALZEME_TABLOSU);


            #endregion

            await Task.FromResult("S");
        }
    }
}
