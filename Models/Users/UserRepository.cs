using Microsoft.EntityFrameworkCore;
using POETestBot.Entities;
using POETestBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POETestBot.Models.Response;

namespace Models.Messages
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

            return (Created , entity.Id);
        }

        public async Task<Response> DeleteAsync(int userId, bool force = false)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(entity == null)
            {
                return NotFound;
            }

            if(entity.Items.Any() && !force)
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
                            Id = u.Id,
                            Name = u.Name
                        };
            return await query.ToListAsync();
        }

        public Task<UserMessagesDTO> ReadAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(UserUpdateDTO user)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> UserExists(int userId, string emailAddress) => await _context.Users.AnyAsync(u => u.Id != userId);
    }
}
