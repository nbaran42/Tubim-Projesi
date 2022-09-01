using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.KurumlarKodTables;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Infrastructure.Repositories.Modules.KurumlarKodTables
{
    public class MaddeTurleriService : BaseGenericRepoKurumlar<KT_MADDETURLERI>, IMaddeTurleriService
    {
        public MaddeTurleriService(KurumlarContext context) : base(context)
        { }
    }
}
