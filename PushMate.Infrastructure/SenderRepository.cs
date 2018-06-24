//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Schema;
//using PushMate.Domain;
//using System;
//using System.Diagnostics;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace PushMate.Infrastructure
//{
//    /// <summary>
//    /// Send messages from your app server to client apps via Firebase Cloud Messaging
//    /// </summary>
//    public class SenderRepository : ISenderRepository
//    {
        
//        /// <summary>
//        /// <summary>
//        /// Send a message Async
//        /// </summary>
//        /// <param name="json">Json Message</param>
//        /// <returns>Response Content</returns>
//        public async Task<ResponseContent> SendAsync(string json)
//        {
//            if (!isValidJsonSyntax(json))
//            {
//                throw new ArgumentException("nameof(json) must be a valid JSON");
//            }

//            if (string.IsNullOrWhiteSpace(json))
//                throw new ArgumentNullException(nameof(json));

//            var requestContent = this.GetRequestContent(json);
//            return await this.SendAsync(requestContent);
//        }


//        /// <summary>
//        /// Send a message Async
//        /// </summary>
//        /// <param name="message">Message</param>
//        /// <returns>Response Content</returns>
//        public async Task<ResponseContent> SendAsync(FcmMessage message)
//        {
//            if (message == null)
//                throw new ArgumentNullException(nameof(message));

//            var requestContent = this.GetRequestContent(message);
//            return await this.SendAsync(requestContent);
//        }

        

//        private HttpContent GetRequestContent(string json)
//        {
//            var content = new StringContent(json, Encoding.UTF8, this._contentType);
//            return content;
//        }

       

       
//    }


//}
