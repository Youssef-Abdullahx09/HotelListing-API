using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Models;


namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Hotel> Hotels
        {
            get
            {
                if (hotels == null)
                    return new Repository<Hotel>(_dbContext);
                else
                    return hotels;
            }
        }
        public IRepository<Country> Countries
        {
            get
            {
                if (countries == null)
                    return new Repository<Country>(_dbContext);
                else
                    return countries;
            }
        }

        private readonly AppDbContext _dbContext;
        private readonly IRepository<Hotel>? hotels;
        private readonly IRepository<Country>? countries;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
