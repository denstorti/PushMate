using Newtonsoft.Json;

namespace PushMate.FcmPushService.DTO
{
    /// <summary>
    /// Priority of the message
    /// </summary>
    public enum Priority
    {
        [JsonProperty("normal")]
        Normal,
        [JsonProperty("high")]
        High
    }
}
