using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace PushMate.FcmPushService.DTO
{
    /// <summary>
    /// Response Message Content
    /// </summary>
    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase{ get; set; }

        public Response(HttpStatusCode statusCode, string reasonPhrase)
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
        }

        /// <summary>
        /// Unique ID (number) identifying the multicast message.
        /// </summary>
        [JsonProperty("multicast_id")]
        public string MulticastId { get; set; }

        /// <summary>
        /// Number of messages that were processed without an error.
        /// </summary>
        [JsonProperty("success")]
        public int Success { get; set; }

        /// <summary>
        /// Number of messages that could not be processed.
        /// </summary>
        [JsonProperty("failure")]
        public int Failure { get; set; }

        /// <summary>
        /// Number of results that contain a canonical registration token. A canonical registration ID is the registration token of the last registration requested by the client app. This is the ID that the server should use when sending messages to the device.
        /// </summary>
        [JsonProperty("canonical_ids")]
        public string CanonicalIds { get; set; }

        /// <summary>
        /// Array of objects representing the status of the messages processed. The objects are listed in the same order as the request (i.e., for each registration ID in the request, its result is listed in the same index in the response).
        /// </summary>
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }
}
