using PushMate.FcmPushService.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PushMate.FcmPushService.Test.Infrastructure
{
    public class IntegrationFcmTest
    {
        private readonly string serverKey = "AIzaSyC8dhbIHM0BEDextBkH1YRGwq2zWSPW2kk";
        private readonly string registrationId = "dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym";

        [Fact]
        public void SendTest_ServerKeyCannotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FcmPushService("")
            );
        }

        [Fact]
        public async Task SendTest_ValidJsonAsync()
        {
            var senderRepository = new FcmPushService(serverKey);
            string validJson = "{ \"registration_ids\":[\"dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym\"],\"priority\":\"Normal\",\"notification\":{\"title\":\"Title\",\"body\":\"Hello World@!23-Jun-18 19:58:47\"}}";

            var result = await senderRepository.SendAsync(validJson);
            Console.WriteLine(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(1, result.Success);
            Assert.Equal("OK", result.ReasonPhrase);
            Assert.NotNull(result.MulticastId);
        }

        [Fact]
        public async Task SendTest_SuccessfulAsync()
        {
            var fcmPushService = new FcmPushService(serverKey);

            var message = GetValidMessage();

            var result = await fcmPushService.SendAsync(message);
            //Assert.IsType<HttpClient>(FcmPushService._httpClient);
            Console.WriteLine(result);
            Assert.True(result.Results.Count >= 1);
            Assert.NotNull(result.MulticastId);
            Assert.Equal(1, result.Success);
        }

        public Message GetValidMessage()
        {
            return new Message
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
