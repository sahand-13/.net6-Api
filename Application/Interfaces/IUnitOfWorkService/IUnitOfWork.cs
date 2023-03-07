using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IUnitOfWorkService
{
    public interface IUnitOfWork
    {
        //sample
        //ITestAsyncRepository Test { get; }
        



        Task CompleteAsync();
        void Dispose();
    }
}
