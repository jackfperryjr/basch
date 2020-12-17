using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Basch.Api.Core.Abstractions;

namespace Basch.Api.Infrastructure.Data
{
    public class FeedStore : IStore<object[]>
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedStore(
            ApplicationDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object[]> AddAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            return reaction;
        }

        public async Task<object[]> UpdateAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            var announcement = await _context.Announcements.FirstOrDefaultAsync(x => x.Id == new Guid(reaction[0].ToString()));

            if (reaction[1].ToString() == "like")
            {
                announcement.Like++;
                reaction[1] = announcement.Like;
            }

            if (reaction[1].ToString() == "dislike")
            {
                announcement.Dislike++;
                reaction[1] = announcement.Dislike;
            }

            if (reaction[1].ToString() == "love")
            {
                announcement.Love++;
                reaction[1] = announcement.Love;
            }

            _context.SaveChanges();
            return reaction;
        }

        public async Task<object[]> DeleteAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            return reaction;
        }
    }
}