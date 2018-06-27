using Moq;
using PushMate.FcmPushService.API.Controllers;
using PushMate.FcmPushService.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PushMate.FcmPushService.API.Test
{
    public class PushApiTest
    {
        private readonly Mock<IHttpPushService> mockHttpPushService;

        public PushApiTest()
        {
            mockHttpPushService = new Mock<IHttpPushService>();
        }

        [Fact]
        public async Task UnitTest_PostAsync()
        {
            var setup = mockHttpPushService.Setup(h => h.SendAsync(It.IsAny<Message>()))
                .ReturnsAsync(new Mock<Response>().Object );
            
            PushController controller = new PushController(mockHttpPushService.Object);


            var response = await controller.PostAsync(GetValidMessage());

            mockHttpPushService.Verify(x => x.SendAsync(It.IsAny<Message>()), Times.Once());

            Assert.NotNull(response);
        }

        public Message GetValidMessage()
        {
            return new Message
            {
                RegistrationIds = new List<string> { "fakefakefakefakefakefakefakefakefakefakefakefake" },
                Notification = new Notification
                {
                    Title = "Title",
                    Body = $"Hello World@!{DateTime.Now.ToString()}"
                }
            };
        }
    }
}
