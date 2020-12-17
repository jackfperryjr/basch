using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Basch.Api.Core.Models;
using Basch.Api.Core.WebApi;
using Basch.Api.Core.Abstractions;

namespace Basch.Api.Controllers.API.V1
{
    [ApiVersion("1")]
    [Authorize]
    public class AnnouncementController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Announcement>, string> _announcementFactory;
        private readonly IStore<object[]> _reactionStore;

        public AnnouncementController(
            IFactory<IQueryable<Announcement>, string> announcementFactory,
            IStore<object[]> reactionStore)
        {
            _announcementFactory = announcementFactory;
            _reactionStore = reactionStore;
        }
        
        [Obsolete]
        [HttpGet("get")]
        public async Task<IActionResult> GetAll(string value, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var announcements = await _announcementFactory.GetAsync(value, cancellationToken);
                if (announcements.Any())
                {
                    return Ok(announcements);
                }
                else 
                {
                    return NotFound(new
                    {
                        message = "There's nothing to show."
                    });                
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [Obsolete]
        [HttpPut("like/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Like(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "like"};
            try
            {
                var count = await _reactionStore.UpdateAsync(reaction, cancellationToken);
                return Ok(count[1]);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Obsolete]
        [HttpPut("dislike/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Dislike(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "dislike"};
            try
            {
                var count = await _reactionStore.UpdateAsync(reaction, cancellationToken);
                return Ok(count[1]);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Obsolete]
        [HttpPut("love/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Love(Guid id, CancellationToken cancellationToken = new CancellationToken()) 
        {    
            object[] reaction = {id, "love"};
            try
            {
                var count = await _reactionStore.UpdateAsync(reaction, cancellationToken);
                return Ok(count[1]);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}