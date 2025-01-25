using Application.Contracts;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class NotificationDatabaseRepository : INotificationDatabaseRepository
    {
        private readonly ApplicationDbContext _context;
        public NotificationDatabaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertNotification(Notification notification)
        {
             _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            
        
        }
    }
}
