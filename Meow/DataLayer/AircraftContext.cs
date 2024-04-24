using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AircraftContext : IDb<Aircraft, int>
    {
        MeowDbContext dbContext;
        public AircraftContext(MeowDbContext dbContext_)
        {
            this.dbContext = dbContext_;
        }


        public async Task CreateAsync(Aircraft item)
        {
            try
            {
                dbContext.Aircrafts.Add(item);
                dbContext.SaveChangesAsync();
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
                Aircraft aircraftFromDb = await ReadAsync(key, false, false);
                dbContext.Aircrafts.Remove(aircraftFromDb);
                dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Aircraft> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Aircraft> query = dbContext.Aircrafts;
                if (useNavigationalProperties)
                {
                    query = query.Include(a => a.Model);
                }
                return await query.FirstOrDefaultAsync(a => a.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Aircraft>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return await dbContext.Aircrafts.Include(a => a.Model).ToListAsync();
                }
                return await dbContext.Aircrafts.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Aircraft item, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                Aircraft aircraftFromDB = await ReadAsync(item.Id, useNavigationalProperties, false);

                if (aircraftFromDB == null) { await CreateAsync(item); }

                dbContext.Entry(aircraftFromDB).CurrentValues.SetValues(item);

                if (useNavigationalProperties)
                {
                    aircraftFromDB.Model = item.Model;
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
