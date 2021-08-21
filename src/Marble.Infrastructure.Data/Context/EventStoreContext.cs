using Marble.Core.Events;
using Microsoft.EntityFrameworkCore;

namespace Marble.Infrastructure.Data.Context
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options)
            : base(options)
        {
        }

        public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}