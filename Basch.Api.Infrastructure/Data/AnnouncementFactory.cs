using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basch.Api.Core.Abstractions;
using Basch.Api.Core.Models;

namespace Basch.Api.Infrastructure.Data
{
    public class AnnouncementFactory : IFactory<IQueryable<Announcement>, string>
    {
        private ApplicationDbContext _context;

        public AnnouncementFactory(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Announcement>> GetAsync(string value, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Announcement> announcements = _context.Announcements;
            announcements = announcements.OrderByDescending(x => x.TimeStamp);
            return announcements;
        }

        public async Task<IQueryable<Announcement>> GetByKeyAsync(string value, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}