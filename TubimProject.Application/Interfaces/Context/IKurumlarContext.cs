using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Interfaces.Context
{
    public interface IKurumlarContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
