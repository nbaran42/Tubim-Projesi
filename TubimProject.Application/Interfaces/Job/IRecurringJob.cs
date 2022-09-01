using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubimProject.Application.Interfaces.Job
{

    public interface IRecurringJob
    {
        string CronExpression { get; }

        string JobId { get; }

        Task Execute();
    }

}
