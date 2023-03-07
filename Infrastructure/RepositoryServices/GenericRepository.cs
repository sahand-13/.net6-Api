using Application.Interfaces.Repository.GenericRepository;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoryServices
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _dbContext;
        internal DbSet<T> dbSet;

        public GenericRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();

        }
        public virtual async Task<T> GetByIdAsync(int id)
        {

            try
            {
                var result = await dbSet.FindAsync(id);
                return result;

            }
            catch (Exception)
            {

                throw new Exception("Error in Database operation");

            }
        }

        public virtual async Task<T> GetByGuidAsync(Guid id)
        {
            try
            {

                var result = await dbSet.FindAsync(id);

                return result;

            }
            catch (Exception e)
            {

                throw new Exception("Error in Database operation");

            }

        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            try
            {

                var result = await _dbContext
                                           .Set<T>()
                                           .Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .AsNoTracking()
                                           .ToListAsync();


                return result;


            }
            catch (Exception)
            {

                throw new Exception("Error in Database operation");

            }
        }

        public async Task<T> AddAsync(T entity)
        {
            string data = JsonConvert.SerializeObject(entity);
            try
            {

                await dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;

            }
            catch (Exception e)
            {
                throw new Exception("Error in Database operation");

            }

        }

        public async Task UpdateAsync(T entity)
        {
            string data = JsonConvert.SerializeObject(entity);

            try
            {

                //_dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
                //_dbContext.ChangeTracker.DetectChanges();
                //_dbContext.Entry(entity).State = EntityState.Modified;
                dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception e)
            {

                throw new Exception("Error in Database operation");


            }

        }

        public async Task DeleteAsync(T entity)
        {
            string data = JsonConvert.SerializeObject(entity);
            try
            {

                dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();


            }
            catch (Exception e)
            {
                throw new Exception("Error in Database operation");
            }

        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {


            try
            {
                return await dbSet.ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception("Error in Database operation");
            }
        }



        public async Task<ICollection<T>> GetByFilterAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {

                var result = await dbSet.Where(predicate).ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw new Exception("Error in Database operation");

            }


        }


        public async Task<T> addListAsync(List<T> entityList)
        {

            try
            {
                foreach (var item in entityList)
                {
                    var test = await dbSet.AddAsync(item);
                }

                await _dbContext.SaveChangesAsync();
                return entityList[0];

            }
            catch (Exception e)
            {
                throw new Exception("Error in Database operation");
            }
        }
        public async Task updateListAsync(List<T> entityList)
        {
            string data = "";
            foreach (var item in entityList)
            {
                data += JsonConvert.SerializeObject(item, Formatting.Indented,
                                       new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }) + ",";
            }
            try
            {
                foreach (var item in entityList)
                {
                    _dbContext.Entry(item).State = EntityState.Modified;
                }
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error in Database operation");
            }
        }
        public async Task DeleteListAsync(List<T> entityList)
        {
            try
            {
                foreach (var item in entityList)
                {
                    dbSet.Remove(item);
                    await _dbContext.SaveChangesAsync();
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error in Database operation");
            }
        }

    }
}
