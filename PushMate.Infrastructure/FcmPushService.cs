using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushMate.FcmPushService.DTO;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PushMate.FcmPushService
{
    public class FcmPushService : IHttpPushService
    {
        public string EndpointUrl => "https://fcm.googleapis.com/fcm/send";
        private string AuthenticationKey;
        private string SenderId;
        private static HttpClient _httpClient;

        public FcmPushService(string authenticationKey, bool mockHttpClient = false)
        {
            if (string.IsNullOrWhiteSpace(authenticationKey))
                throw new ArgumentNullException(nameof(authenticationKey));

            AuthenticationKey = authenticationKey;

            if (_httpClient == null && mockHttpClient == false)
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={authenticationKey}");
                _httpClient = httpClient;
            }
            else
            {
                HttpClient httpClient = new HttpClientMock();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={authenticationKey}");
                _httpClient = httpClient;
            }

        }

        public FcmPushService(string authenticationKey, string senderId, bool mockHttpClient = false) : this(authenticationKey, mockHttpClient)
        {
            if (string.IsNullOrWhiteSpace(senderId))
                throw new ArgumentNullException(nameof(senderId));

            SenderId = senderId;
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
