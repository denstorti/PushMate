using Newtonsoft.Json;
using PushMate.FcmPushService.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PushMate.FcmPushService
{
    class FormatMessage
    {
        public static string ContentType => "application/json";

        public static async Task<Response> GetResponseContentAsync(HttpResponseMessage response)
        {
            string json = string.Empty;
            try
            {
                json = await response.Content.ReadAsStringAsync();
                var messageResponse = JsonConvert.DeserializeObject<Response>(json);
                messageResponse.StatusCode = response.StatusCode;
                messageResponse.ReasonPhrase = response.ReasonPhrase;
                return messageResponse;
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException("Error during the deserialization of the response: StatusCode: {"+response.StatusCode+ "}, Content : {" + response.Content + "}", ex);
            }
        }

        public static HttpContent GetRequestContent(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, ContentType);
            return content;
        }

        public static HttpContent GetRequestContent(Message message)
        {
            string json = JsonConvert.SerializeObject(message, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                            });

            return GetRequestContent(json);
        }
    }
}
