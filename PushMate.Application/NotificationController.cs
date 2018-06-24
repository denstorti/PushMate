using Microsoft.Extensions.Logging;
using PushMate.Domain;
using PushMate.Domain.Interfaces;
using PushMate.Infrastructure;
using PushMate.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PushMate.Application
{
    class NotificationController
    {
        private readonly IHttpPushService senderRepository;
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(IHttpPushService senderRepository, ILoggerFactory loggerFactory)
        {
            this.senderRepository = senderRepository;
            _logger = loggerFactory.CreateLogger<NotificationController>();
        }

        public async Task<IResponse> SendOnePushAndroid()
        {
            _logger.LogInformation("before sending");
            FcmMessage fcmMessage = new FcmMessage() { To = "dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym", Data = "test" };

            return await senderRepository.SendAsync(fcmMessage);
            
        }
        
    }
}
