using PushMate.FcmPushService.Domain.Interfaces;

namespace PushMate.FcmPushService.Domain
{
    /// <summary>
    /// Result Item
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// String specifying a unique ID for each successfully processed message.
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Optional string specifying the canonical registration token for the client app that the message was processed and sent to. Sender should use this value as the registration token for future requests. Otherwise, the messages might be rejected.
        /// </summary>
        public string RegistrationId { get; set; }

        /// <summary>
        /// String specifying the error that occurred when processing the message for the recipient. The possible values can be found in https://firebase.google.com/docs/cloud-messaging/http-server-ref#table9
        /// </summary>
        public string Error { get; set; }
    }
}
