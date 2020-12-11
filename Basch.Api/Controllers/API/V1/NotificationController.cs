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
    public class NotificationController : ApiControllerBase
    {
        private readonly IFactory<IQueryable<Notification>, string> _notificationFactory;

        public NotificationController(
            IFactory<IQueryable<Notification>, string> notificationFactory)
        {
            _notificationFactory = notificationFactory;
        }
        
        [Obsolete]
        [HttpGet("get")]
        public async Task<IActionResult> GetAll(string value, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var notifications = await _notificationFactory.GetAsync(value, cancellationToken);
                if (notifications.Any())
                {
                    return Ok(notifications);
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
    }
}