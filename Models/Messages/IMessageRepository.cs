using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POETestBot.Models
{
    interface IMessageRepository
    {
        Task<(Response response, int messageID )> CreateAsync(UserCreateDTO user);

        Task<IEnumerable<MessageListDTO>> ReadAsync();

        Task<MessageDetailsDTO> ReadAsync(int messageID);
        Task<Response> DeleteAsync(int MessageID, bool force = false);

        //TODO maybe exchange for commands
    }
}
