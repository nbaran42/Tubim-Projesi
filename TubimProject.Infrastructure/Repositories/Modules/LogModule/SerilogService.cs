using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.Interfaces.Repositories;
using TubimProject.Application.Interfaces.Repositories.Modules.LogModule;
using TubimProject.Domain.Entities.Logging;

namespace TubimProject.Infrastructure.Repositories.Modules.LogModule
{
    public class SerilogService : BaseGenericRepo<Logs>, ISeriLogService
    {
        public SerilogService(ApplicationDbContext context) : base(context)
        { }
    }
}
