using Moq;
using PushMate.Domain;
using PushMate.Domain.Interfaces;
using PushMate.Infrastructure;
using PushMate.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PushMate.Test.Infrastructure
{
    public class SendTest
    {
        private readonly string serverKey = "AIzaSyC8dhbIHM0BEDextBkH1YRGwq2zWSPW2kk";
        private readonly string registrationId = "dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym";
        
        //[Fact]
        //public async Task SendTest_InvalidJson()
        //{
        //    var senderRepository = new FcmPushService(serverKey);

        //    await Assert.ThrowsAsync<ArgumentException>(()=>
        //        senderRepository.SendAsync("invalidJson")
        //    );

        //}

        //[Fact]
        //public void SendTest_ValidJson()
        //{
        //    var senderRepository = new FcmPushService(serverKey);
        //    string validJson = "{ \"registration_ids\":[\"dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym\"],\"priority\":\"Normal\",\"notification\":{\"title\":\"Title\",\"body\":\"Hello World@!23-Jun-18 19:58:47\"}}";

        //    var result = senderRepository.SendAsync(validJson).Result;

        //    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        //}

        [Fact]
        public void SendTest_Successful()
        {
            var fcmPushService = new FcmPushService(serverKey);

            var message = GetValidMessage();
            var result = fcmPushService.SendAsync(message).Result;
            Assert.True(result.Results.Count >= 1);
            Assert.NotNull(result.MulticastId);
            Assert.Equal(1, result.Success);
        }

        [Fact]
        public void SendTest_SenderIdCannotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => 
                new FcmPushService(serverKey, "")
            );
            
        }

        public IMessage GetValidMessage()
        {
            return new FcmMessage
            {
                RegistrationIds = new List<string> { registrationId },
                Notification = new Notification
                {
                    Title = "Title",
                    Body = $"Hello World@!{DateTime.Now.ToString()}"
                }
            };
        }
    }
}
