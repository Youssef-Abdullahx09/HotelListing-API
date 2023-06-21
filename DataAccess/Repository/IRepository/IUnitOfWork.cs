using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Hotel> Hotels { get; }
        IRepository<Country> Countries { get; }

        Task Save();
    }
}
