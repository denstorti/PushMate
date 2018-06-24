using Newtonsoft.Json.Linq;
using PushMate.FcmPushService.DTO;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PushMate.FcmPushService
{
    public class FcmPushService : IHttpPushService
    {
        public string EndpointUrl => "https://fcm.googleapis.com/fcm/send";
        public HttpClient _httpClient;   //PROBLEM HERE: The HttpClient must be static to be always reused, but with unit tests the factory fails because one test override the other

        /// <summary>
        /// If the HttpClient is not injected, it uses the DefaultHttpClientAccessor
        /// </summary>
        /// <param name="authenticationKey">Server key from FCM account</param>
        /// <param name="senderId">SenderId drom FCM account</param>
        public FcmPushService(string authenticationKey, string senderId = null)
        {
            var defaultHttpClientAccessor = new DefaultHttpClient();
            _httpClient = defaultHttpClientAccessor.Create(authenticationKey, senderId);
            
        }

        /// <summary>
        /// Constructor for FCM push notifications. Mocking objects may be passed in here as MockedHttpClientOK.
        /// </summary>
        /// <param name="authenticationKey"></param>
        /// <param name="senderId"></param>
        /// <param name="httpClientAccessor"></param>
        public FcmPushService(string authenticationKey, string senderId, IHttpClientFactory httpClientAccessor)
        {
            _httpClient = httpClientAccessor.Create(authenticationKey, senderId);
        }
        
        private bool isValidJsonSyntax(string json)
        {
            try
            {
                JObject.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Response> SendAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var requestContent = FormatMessage.GetRequestContent(message);
            return await SendAsync(requestContent);
        }

        public async Task<Response> SendAsync(string json)
        {
            if (!isValidJsonSyntax(json))
            {
                throw new ArgumentException("nameof(json) must be a valid JSON");
            }

            if (string.IsNullOrWhiteSpace(json))
                throw new ArgumentNullException(nameof(json));

            var requestContent = FormatMessage.GetRequestContent(json);
            return await SendAsync(requestContent);
        }

        private async Task<Response> SendAsync(HttpContent content)
        {
            var response = await _httpClient.PostAsync(EndpointUrl, content);
            Response result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = await FormatMessage.GetResponseContentAsync(response);
            }
            else
            {
                result = new Response(response.StatusCode, response.ReasonPhrase);
            }
            return result;
        }


    }
}
