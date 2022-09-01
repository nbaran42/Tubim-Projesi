using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.MaddeModule;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Infrastructure.Repositories.Modules.MaddeModule
{
    public class MaddeService:BaseGenericRepoKurumlar<UT_MADDE>,IMaddeService
    {
        public MaddeService(KurumlarContext context):base(context)
        {}
    }
}
