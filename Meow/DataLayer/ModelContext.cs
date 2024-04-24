using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ModelContext : IDb<Model, int>
    {
        MeowDbContext dbContext;
        public ModelContext(MeowDbContext dbContext_)
        {
            this.dbContext = dbContext_;
        }


        public async Task CreateAsync(Model item)
        {
            try
            {
                dbContext.Models.Add(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Model modelFromDb = await ReadAsync(key, false, false);
                dbContext.Models.Remove(modelFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Model> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                return dbContext.Models.Find(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Model>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                return dbContext.Models.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Model item, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                dbContext.Models.Update(item);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
