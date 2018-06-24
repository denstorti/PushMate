using Newtonsoft.Json;
using PushMate.Domain.Interfaces;

namespace PushMate.Infrastructure.DTO
{
    /// <summary>
    /// Result Item
    /// </summary>
    public class FcmResult: IResult
    {
        /// <summary>
        /// String specifying a unique ID for each successfully processed message.
        /// </summary>
        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        /// <summary>
        /// Optional string specifying the canonical registration token for the client app that the message was processed and sent to. Sender should use this value as the registration token for future requests. Otherwise, the messages might be rejected.
        /// </summary>
        [JsonProperty("registration_id")]
        public string RegistrationId { get; set; }

        /// <summary>
        /// String specifying the error that occurred when processing the message for the recipient. The possible values can be found in https://firebase.google.com/docs/cloud-messaging/http-server-ref#table9
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
