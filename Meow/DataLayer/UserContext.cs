using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BussinessLayer;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserContext : IDb<User, int>
    {
        MeowDbContext dBContext;

        public UserContext(MeowDbContext dBContext_)
        {
            this.dBContext = dBContext_;
        }

        public async Task CreateAsync(User user)
        {
            try
            {
                dBContext.Users.Add(user);
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
                User userFromDB = await ReadAsync(key, false, false);
                dBContext.Users.Remove(userFromDB);
                await dBContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<User> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                return dBContext.Users.Find(key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<User>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                return dBContext.Users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(User item, bool useNavigationalProperties = false, bool isReadOnly = false)
        {
            try
            {
                dBContext.Users.Update(item);
                await dBContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
