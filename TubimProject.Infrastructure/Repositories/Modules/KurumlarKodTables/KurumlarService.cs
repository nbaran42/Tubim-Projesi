using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories.Modules.KurumlarKodTables;
using TubimProject.Domain.Entities.Tpds;

namespace TubimProject.Infrastructure.Repositories.Modules.KurumlarKodTables
{
    public class KurumlarService : BaseGenericRepoKurumlar<KT_KURUMLAR>, IKurumlarService
    {
        public KurumlarService(KurumlarContext context) : base(context)
        { }
    }
}
