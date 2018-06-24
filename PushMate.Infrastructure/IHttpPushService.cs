using PushMate.Domain.Interfaces;
using System.Threading.Tasks;

namespace PushMate.Infrastructure
{
    public interface IHttpPushService
    {
        string EndpointUrl { get; }
        string AuthenticationKey { get; }
        string SenderId { get; }
        string ContentType { get; }
        Task<IResponse> SendAsync(IMessage message);
    }
}
