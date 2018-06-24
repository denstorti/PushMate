using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushMate.Domain;
using PushMate.Domain.Interfaces;
using PushMate.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PushMate.Infrastructure
{
    public class FcmPushService : IHttpPushService
    {
        public string EndpointUrl => "https://fcm.googleapis.com/fcm/send";
        public string ContentType => "application/json";
        public string AuthenticationKey { get; set; }
        public string SenderId { get; }

        private static readonly HttpClient _httpClient = new HttpClient();

        public FcmPushService(string authenticationKey)
        {
            if (string.IsNullOrWhiteSpace(authenticationKey))
                throw new ArgumentNullException(nameof(authenticationKey));

            AuthenticationKey = authenticationKey;
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={AuthenticationKey}");
        }

        public FcmPushService(string authenticationKey, string senderId) : this(authenticationKey)
        {
            if (string.IsNullOrWhiteSpace(senderId))
                throw new ArgumentNullException(nameof(senderId));

            SenderId = senderId;
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={senderId}");
        }

        public bool isValidJsonSyntax(string json)
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

        public async Task<IResponse> SendAsync(IMessage message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var requestContent = this.GetRequestContent(message);
            return await SendAsync(requestContent);
        }

        private async Task<IResponse> SendAsync(HttpContent content)
        {
            var response = await _httpClient.PostAsync(this.EndpointUrl, content);
            FcmResponse result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await GetResponseContentAsync(response);
            }
            else
            {
                return new FcmResponse(response.StatusCode, response.ReasonPhrase);
            }

        }

        private async Task<IResponse> GetResponseContentAsync(HttpResponseMessage response)
        {
            string json = string.Empty;
            try
            {
                json = await response.Content.ReadAsStringAsync();
                var messageResponse = JsonConvert.DeserializeObject<FcmResponse>(json);
                return messageResponse;
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException("Error during the deserialization of the response", ex);

                //return new MessageResponse { InternalError = ex.Message, ResponseContent = json };
            }
        }

        private HttpContent GetRequestContent(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, this.ContentType);
            return content;
        }

        private HttpContent GetRequestContent(IMessage message)
        {
            string json = JsonConvert.SerializeObject(message, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                            });

            return this.GetRequestContent(json);
        }

    }
}
