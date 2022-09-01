using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.SucTanimModule;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Infrastructure.Repositories.Modules.SucTanimModule
{
    public class SucTanimService:BaseGenericRepoKurumlar<UT_SUCTANIM>,ISucTanimService
    {
        public SucTanimService(KurumlarContext context):base(context)
        {   }
    }
}
