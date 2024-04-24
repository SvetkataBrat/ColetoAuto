using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CarContext : IDb<Car, int>
    {
        MeowDbContext dbContext;

        public CarContext(MeowDbContext dBContext_)
        {
            this.dbContext = dBContext_;
        }

        public async Task CreateAsync(Car item)
        {
            try
            {
                dbContext.Add(item);
                dbContext.SaveChanges();
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
                Car carFromDb = dbContext.Cars.Find(key);
                if (carFromDb != null)
                {
                    dbContext.Cars.Remove(carFromDb);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Car> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Car> query = dbContext.Cars;
                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.Model);
                }
                return await query.FirstOrDefaultAsync(x => x.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Car>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Car> query = dbContext.Cars;
                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.Model);
                }
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Car item, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                Car carFromDB = await ReadAsync(item.Id, useNavigationalProperties, false);

                if (carFromDB == null) { await CreateAsync(item); }

                dbContext.Entry(carFromDB).CurrentValues.SetValues(item);

                if (useNavigationalProperties)
                {
                    carFromDB.Model = item.Model;
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
