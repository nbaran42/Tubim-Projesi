using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TubimProject.Application.DTOs.Services.DeserializeModels;
using TubimProject.Application.Enums;

namespace TubimProject.Application.Interfaces.WebServices
{
    public interface IServiceContext
    {
        JGetToken? GetToken();
        JGetYetkiOlunanTablolar? GetYetkiOlunanTablolar(string token);
        T GetServiceData<T>(long? lastId, JTabloEnums jTabloEnums);
    }
}
