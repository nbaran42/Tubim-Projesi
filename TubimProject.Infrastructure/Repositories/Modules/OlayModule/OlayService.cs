using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayModule;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Infrastructure.Repositories.Modules.OlayModule
{
    public class OlayService : BaseGenericRepoKurumlar<UT_OLAY>, IOlayService
    {
        public OlayService(KurumlarContext context) : base(context)
        { }
    }
}
