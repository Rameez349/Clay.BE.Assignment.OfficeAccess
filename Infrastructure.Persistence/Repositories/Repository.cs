using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly OfficeAccessDbContext _officeAccessDbContext;

        public Repository(OfficeAccessDbContext officeAccessDbContext)
        {
            _officeAccessDbContext = officeAccessDbContext;
        }

        public async Task DeleteAsync(T entity)
        {
            _officeAccessDbContext.Remove(entity);
            await _officeAccessDbContext.SaveChangesAsync();
        }

        public async Task InsertAsync(T entity)
        {
            _officeAccessDbContext.Add(entity);
            await _officeAccessDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _officeAccessDbContext.Update(entity);
            await _officeAccessDbContext.SaveChangesAsync();
        }
    }
}
