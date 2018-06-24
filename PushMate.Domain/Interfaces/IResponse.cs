using System;
using System.Collections.Generic;

namespace PushMate.FcmPushService.Domain.Interfaces
{
    public interface IResponse
    {
        string CanonicalIds { get; set; }
        int Failure { get; set; }
        string MulticastId { get; set; }
        List<IResult> Results { get; set; }
        int Success { get; set; }
    }
}