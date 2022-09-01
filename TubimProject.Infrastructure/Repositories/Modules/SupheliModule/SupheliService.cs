using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.MaddeModule;
using TubimProject.Domain.Entities.Tpds;


namespace TubimProject.Infrastructure.Repositories.Modules.SupheliModule
{
    public class SupheliService : BaseGenericRepoKurumlar<UT_SAHIS>, Application.Interfaces.Repositories.Modules.SupheliModule.ISupheliService
    {
        public SupheliService(KurumlarContext context) : base(context)
        { }
    }
}
