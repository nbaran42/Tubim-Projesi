using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Common;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Application.Mappings
{
    public class KodTablesProfile:Profile
    {
        public KodTablesProfile()
        {
            CreateMap<KT_MADDETURLERI, KodTablesResponse>().ReverseMap();
            CreateMap<KT_KURUMLAR, KodTablesResponse>().ReverseMap();
        }
    }
}
