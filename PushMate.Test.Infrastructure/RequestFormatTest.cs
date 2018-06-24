using PushMate.FcmPushService.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PushMate.FcmPushService.Test.Infrastructure
{
    public class RequestFormatTest
    {
        private readonly string serverKey = "fakeServerKey";
        private readonly string registrationId = "fakeRegistrationId";

        [Fact]
        public void SendTest_SenderCannotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FcmPushService(serverKey, "")
            );
        }

        [Fact]
        public void SendTest_Extensions()
        {
        }

        [Fact]
        public void SendTest_InvalidJson()
        {
            var senderRepository = new FcmPushService(serverKey);
            
            Assert.ThrowsAsync<ArgumentException>(async () =>
                await senderRepository.SendAsync("invalidJson")
            );

        }

        [Fact]
        public async Task SendTest_ValidJsonAsync()
        {
            var senderRepository = new FcmPushService(serverKey, true);
            string validJson = "{ \"registration_ids\":[\"dG4rFnirWOE:APA91bE3COnsY-flnulPse4b4uKZOUDRpdOAe6DGTU_jWGtJt0P_hBXoN1tOa9Je4ZyAfA11OS3US0fZm6M7EljYipCY1f4MqjDLLvEltfe8_3aDnzwTxRbuw23HQ2JIY2ihXQXUvDym\"],\"priority\":\"Normal\",\"notification\":{\"title\":\"Title\",\"body\":\"Hello World@!23-Jun-18 19:58:47\"}}";

            var result = await senderRepository.SendAsync(validJson);
            Console.WriteLine(result);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(1, result.Success);
            Assert.Single(result.Results);
            Assert.Equal("OK", result.ReasonPhrase);
            Assert.Equal("4674514773536739316",result.MulticastId);
            Assert.Equal("0:1529817872903218 % 3a7f5afa3a7f5afa", result.Results[0].MessageId);
        }

        [Fact]
        public async Task SendTest_SuccessfulAsync()
        {
            var fcmPushService = new FcmPushService(serverKey);

            var message = GetValidMessage();

            var result = await fcmPushService.SendAsync(message);
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
