using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Features.Madde.Queries.GetAllMaddeQuery;
using TubimProject.Application.Features.Olay.Command;
using TubimProject.Application.Features.Olay.Queries.GetAllOlaylar;
using TubimProject.Application.Features.OlayDetay.Command;
using TubimProject.Application.Features.OlayDetay.Queries.GetAllOlayDetaysQuery;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Application.Mappings
{
    public class TpdsProfile:Profile
    {
        public TpdsProfile()
        {
            CreateMap<UT_OLAY, GetAllOlaysQueryResponse>().ReverseMap();
            CreateMap<UT_OLAY, CreateOlayKayitCommand>().ReverseMap();
            CreateMap<UT_OLAYDETAY, GetAllOlayDetaysQueryResponse>().ReverseMap();
            CreateMap<UT_OLAYDETAY, CreateOlayDetayKayitCommand>().ReverseMap();
            CreateMap<UT_MADDE, GetAllMaddeQueryResponse>().ReverseMap();
        }
    }
}
