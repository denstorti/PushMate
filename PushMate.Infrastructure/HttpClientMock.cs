using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PushMate.FcmPushService
{
    public class HttpClientMock: HttpClient
    {
        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            string validResponseJson = "{\"multicast_id\":4674514773536739316,\"success\":1,\"failure\":0,\"canonical_ids\":0,\"results\":[{\"message_id\":\"0:1529817872903218 % 3a7f5afa3a7f5afa\"}]}";
            response.Content = new StringContent(validResponseJson);

            return response;

        }
    }
}
