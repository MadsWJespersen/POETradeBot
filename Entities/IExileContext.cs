using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace POETestBot.Entities
{
    public interface IExileContext
    {
        DbSet<Message> Messages { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
