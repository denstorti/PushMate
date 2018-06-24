using PushMate.FcmPushService.Domain.Interfaces;
using System.Collections.Generic;

namespace PushMate.FcmPushService.Domain
{
    /// <summary>
    /// Response Message Content
    /// </summary>
    public class Response : IResponse
    {
        /// <summary>
        /// Internal Error
        /// </summary>
        public string InternalError { get; set; }

        /// <summary>
        /// Response Content
        /// </summary>
        public string ResponseContent { get; set; }

        /// <summary>
        /// Unique ID (number) identifying the multicast message.
        /// </summary>
        public string MulticastId { get; set; }

        /// <summary>
        /// Number of messages that were processed without an error.
        /// </summary>
        public int Success { get; set; }

        /// <summary>
        /// Number of messages that could not be processed.
        /// </summary>
        public int Failure { get; set; }

        /// <summary>
        /// Number of results that contain a canonical registration token. A canonical registration ID is the registration token of the last registration requested by the client app. This is the ID that the server should use when sending messages to the device.
        /// </summary>
        public string CanonicalIds { get; set; }

        /// <summary>
        /// Array of objects representing the status of the messages processed. The objects are listed in the same order as the request (i.e., for each registration ID in the request, its result is listed in the same index in the response).
        /// </summary>
        public List<IResult> Results { get; set; }
    }
}
