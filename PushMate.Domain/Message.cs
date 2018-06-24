using PushMate.Domain.Interfaces;
using System.Collections.Generic;

namespace PushMate.Domain
{
    /// <summary>
    /// Keys for messages
    /// </summary>
    public class Message : IMessage
    {
        /// <summary>
        /// This parameter specifies the recipient of a message.
        /// <para/>The value must be a registration token, notification key, or topic. Do not set this field when sending to multiple topics. 
        ///<para/>See <seealso cref="PushMate.Domain.Net.Message.Condition"/>
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// This parameter specifies a list of devices (registration tokens, or IDs) receiving a multicast message. It must contain at least 1 and at most 1000 registration tokens.
        /// <para/>Use this parameter only for multicast messaging, not for single recipients. Multicast messages (sending to more than 1 registration tokens) are allowed using HTTP JSON format only. 
        /// </summary>
        public List<string> RegistrationIds { get; set; }

        /// <summary>
        /// This parameter specifies a logical expression of conditions that determine the message target.
        /// <para/>Supported condition: Topic, formatted as "'yourTopic' in topics". This value is case-insensitive.
        /// <para/>Supported operators: &amp;&amp;, ||. Maximum two operators per topic message supported.
        /// </summary>
        public string Condition { get; set; }

        /// <summary>
        /// Sets the priority of the message. Valid values are "normal" and "high." On iOS, these correspond to APNs priorities 5 and 10.
        /// <para/>By default, messages are sent with normal priority.Normal priority optimizes the client app's battery consumption and should be used unless immediate delivery is required. 
        /// <para/>For messages with normal priority, the app may receive the message with unspecified delay.
        /// <para/>When a message is sent with high priority, it is sent immediately, and the app can wake a sleeping device and open a network connection to your server.
        /// </summary>
        public string CollapseKey { get; set; }

        /// <summary>
        /// Sets the priority of the message. Valid values are "normal" and "high." On iOS, these correspond to APNs priorities 5 and 10.
        /// <para/>By default, messages are sent with normal priority.Normal priority optimizes the client app's battery consumption and should be used unless immediate delivery is required. For messages with normal priority, the app may receive the message with unspecified delay.
        /// <para/>When a message is sent with high priority, it is sent immediately, and the app can wake a sleeping device and open a network connection to your server.
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// On iOS, use this field to represent content-available in the APNs payload. When a notification or message is sent and this is set to true, an inactive client app is awoken. On Android, data messages wake the app by default. On Chrome, currently not supported.
        /// </summary>
        public bool? ContentAvailable { get; set; }

        /// <summary>
        /// This parameter specifies how long (in seconds) the message should be kept in FCM storage if the device is offline. The maximum time to live supported is 4 weeks, and the default value is 4 weeks. 
        /// </summary>
        public int? TimeToLive { get; set; }

        /// <summary>
        /// This parameter specifies the package name of the application where the registration tokens must match in order to receive the message.
        /// </summary>
        public string RestrictedPackageName { get; set; }

        /// <summary>
        /// This parameter, when set to true, allows developers to test a request without actually sending a message.
        /// <para/>The default value is false.
        /// </summary>
        public bool? DryRun { get; set; }

        /// <summary>
        /// This parameter specifies the custom key-value pairs of the message's payload.
        /// <para/>For example, with data:{"score":"3x1"}:
        /// <para/>On iOS, if the message is sent via APNS, it represents the custom data fields.If it is sent via FCM connection server, it would be represented as key value dictionary in AppDelegate application:didReceiveRemoteNotification:.
        /// <para/>On Android, this would result in an intent extra named score with the string value 3x1.
        /// <para/>The key should not be a reserved word ("from" or any word starting with "google" or "gcm"). Do not use any of the words defined in this table(such as collapse_key).
        /// <para/>Values in string types are recommended.You have to convert values in objects or other non-string data types (e.g., integers or booleans) to string.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// This parameter specifies the predefined, user-visible key-value pairs of the notification payload. See Notification payload support for detail. For more information about notification message and data message options, see Payload.
        /// </summary>
        public INotification Notification { get; set; }
    }
}
