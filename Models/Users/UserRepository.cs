using Microsoft.EntityFrameworkCore;
using POETestBot.Entities;
using POETestBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POETestBot.Models.Response;

namespace POETestBot.Models
{
    class UserRepository : IUserRepository
    {

        private readonly IExileContext _context;

        public UserRepository(IExileContext context)
        {
            this._context = context;
        }


        public async Task<(Response response, int userId)> CreateAsync(UserCreateDTO user)
        {
            if (await UserExists(0, user.Name))
            {
                return (Conflict, 0);
            }

            var entity = new User
            {
                Name = user.Name
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            return (Created , entity.ID);
        }

        public async Task<Response> DeleteAsync(int userId, bool force = false)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.ID == userId);
            if(entity == null)
            {
                return NotFound;
            }

            if (entity.Items.Any() && !force)
            {
                return Conflict;
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();

            return Deleted;

        }

        public async Task<IEnumerable<UserListDTO>> ReadAsync()
        {
            var query = from u in _context.Users
                        orderby u.Name
                        select new UserListDTO
                        {
                            Id = u.ID,
                            Name = u.Name
                        };
            return await query.ToListAsync();
        }

        public async Task<UserMessagesDTO> ReadMessagesAsync(int userId)
        {
            var userMessagesDTO = from u in _context.Users
                        where u.ID == userId
                        select new UserMessagesDTO
                        {
                            UserID = u.ID,
                            Name = u.Name,
                            Messages = u.Messages
                        };

            return await userMessagesDTO.FirstOrDefaultAsync();
        }

        public async Task<UserItemsDTO> ReadItemsAsync(int userId)
        {
            var userItemsDTO = from u in _context.Users
                                  where u.ID == userId
                                  select new UserItemsDTO
                                  {
                                      UserID = u.ID,
                                      Name = u.Name,
                                      Items = u.Items
                                  };

            return await userItemsDTO.FirstOrDefaultAsync();
        }

        public async Task<Response> UpdateAsync(UserUpdateDTO user)
        {
            var entity = await _context.Users.FindAsync(user.Name);

            if (entity == null)
            {
                return NotFound;
            }

            entity.passwordHash = user.passwordHash;

            await _context.SaveChangesAsync();

            return Updated;
        }

        private async Task<bool> UserExists(int userId, string username) => await _context.Users.AnyAsync(u => u.ID != userId && u.Name == username);
    }
}
