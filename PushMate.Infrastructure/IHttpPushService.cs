using PushMate.FcmPushService.DTO;
using System.Threading.Tasks;

namespace PushMate.FcmPushService
{
    public interface IHttpPushService
    {
        Task<Response> SendAsync(Message message);
    }
}
