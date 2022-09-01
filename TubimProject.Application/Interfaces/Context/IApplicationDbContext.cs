using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TubimProject.Domain.Entities.Logging;

namespace TubimProject.Application.Interfaces.Context
{
    public interface IApplicationDbContext
    {
      

       
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
       

    }
}
