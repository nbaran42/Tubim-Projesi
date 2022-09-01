using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.OlayDetayModule;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Infrastructure.Repositories.Modules.OlayDetayModule
{
    public class OlayDetayService:BaseGenericRepoKurumlar<UT_OLAYDETAY>,IOlayDetayService
    {
        public OlayDetayService(KurumlarContext context) : base(context)    
        {}
    }
}
