using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context=context;
        }
        public void Add<T>(T entity) where T : class
        {
           
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userid)
        {
            return await _context.Photos.Where(u =>u.UserId == userid).FirstOrDefaultAsync(p=> p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var Photo= await _context.Photos.FirstOrDefaultAsync(p=>p.Id==id);
            return Photo;

        }

        public async Task<User> GetUser(int id)
        {
           var user= await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(user=>user.Id==id);
           return user;
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
         var users= await _context.Users.Include(p=>p.Photos).ToListAsync();
         return users;

        }

        public async  Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0 ;
        }
    }
}