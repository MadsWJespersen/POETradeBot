using POETestBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POETestBot.Models
{
    interface IItemRepository
    {
        Task<(Response response, int itemID)> CreateAsync(ItemCreateDTO user);
        Task<IEnumerable<ItemListDTO>> ReadAsync();
        Task<Response> UpdateAsync(ItemUpdateDTO item);
        Task<Response> DeleteAsync(int itemID, bool force = false);
    }
}
