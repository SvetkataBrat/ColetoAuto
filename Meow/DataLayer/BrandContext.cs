using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BrandContext : IDb<Brand, int>
    {
       
 
        MeowDbContext dBContext;

        public BrandContext(MeowDbContext dBContext_)
        {
            this.dBContext = dBContext_;
        }

        public async Task CreateAsync(Brand brand)
        {
            try
            {
                dBContext.Brands.Add(brand);
                await dBContext.SaveChangesAsync();
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
                Brand brandFromDB = await ReadAsync(key, false, false);  
                dBContext.Brands.Remove(brandFromDB);
                await dBContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Brand> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                return dBContext.Brands.Find(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Brand>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                return dBContext.Brands.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Brand item, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                dBContext.Brands.Update(item);
                await dBContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
