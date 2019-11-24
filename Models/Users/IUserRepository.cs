using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POETestBot.Models
{
    interface IUserRepository
    {
        Task<(Response response, int userId)> CreateAsync(UserCreateDTO user);
        Task<IEnumerable<UserListDTO>> ReadAsync();
        Task<UserMessagesDTO> ReadAsync(int userId);
        Task<Response> UpdateAsync(UserUpdateDTO user);
        Task<Response> DeleteAsync(int userId, bool force = false);
    }
}
