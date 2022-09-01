using Hangfire;

namespace TubimProject.UI.Jobs.Interfaces
{
    public interface ISetOlayCacheJob
    {
        [JobDisplayName("Olay Bilgilerini Cache Sistemine Eklenmesi")]
        [AutomaticRetry(Attempts = 5)] 
        void Run();
    }
}
