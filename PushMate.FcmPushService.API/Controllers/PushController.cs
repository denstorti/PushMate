using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PushMate.FcmPushService.DTO;

namespace PushMate.FcmPushService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushController : ControllerBase
    {
        private readonly IHttpPushService httpPushService;

        public PushController(IHttpPushService httpPushService)
        {
            this.httpPushService = httpPushService;
        }

        // POST: api/Push
        [HttpGet]
        public string Get()
        {
            return "Alow";
        }

        // POST: api/Push
        [HttpPost]
        public async Task<Response> PostAsync([FromBody] Message message)
        {
            Response response = await httpPushService.SendAsync(message);

            return response;
        }

    }
}
