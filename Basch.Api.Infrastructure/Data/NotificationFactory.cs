using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basch.Api.Core.Abstractions;
using Basch.Api.Core.Models;

namespace Basch.Api.Infrastructure.Data
{
    public class NotificationFactory : IFactory<IQueryable<Notification>, string>
    {
        private ApplicationDbContext _context;

        public NotificationFactory(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Notification>> GetAsync(string value, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Notification> notifications = _context.Notifications
                                                        .Where(x => x.Active == 1);
            return notifications;
        }

        public async Task<IQueryable<Notification>> GetByKeyAsync(string value, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}