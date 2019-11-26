using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POETestBot.Models
{
    class ItemRepository : IItemRepository
    {
        public Task<(Response response, int itemID)> CreateAsync(ItemCreateDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteAsync(int itemID, bool force = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemListDTO>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAsync(ItemUpdateDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
