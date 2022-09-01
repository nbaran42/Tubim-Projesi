using TubimProject.Application.Interfaces.Repositories.Modules.LogModule;
using TubimProject.Domain.Entities.Common;

namespace TubimProject.Infrastructure.Repositories.Modules.LogModule
{
    public class CrashReportService : BaseGenericRepo<UT_CRASHREPORT>, ICrashReportService
    {
        public CrashReportService(ApplicationDbContext context) : base(context)
        { }
    }
}

