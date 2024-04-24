using BussinessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BoatContext : IDb<Boat, int>
    {
        MeowDbContext dbContext;
        public BoatContext(MeowDbContext dbContext_)
        {
            this.dbContext = dbContext_;
        }


        public async Task CreateAsync(Boat item)
        {
            try
            {
                dbContext.Boats.Add(item);
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
                Boat boatFromDb = await ReadAsync(key, false, false);
                dbContext.Boats.Remove(boatFromDb);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Boat> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Boat> query = dbContext.Boats;
                if (useNavigationalProperties)
                {
                    query = query.Include(b => b.Model);
                }
                return await query.FirstOrDefaultAsync(b => b.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Boat>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                IQueryable<Boat> query = dbContext.Boats;
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

        public async Task UpdateAsync(Boat item, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                Boat boatFromDB = await ReadAsync(item.Id, useNavigationalProperties, false);

                if (boatFromDB == null) { await CreateAsync(item); }

                dbContext.Entry(boatFromDB).CurrentValues.SetValues(item);

                if (useNavigationalProperties)
                {
                    boatFromDB.Model = item.Model;
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

