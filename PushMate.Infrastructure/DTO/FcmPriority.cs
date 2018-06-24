using Newtonsoft.Json;

namespace PushMate.Infrastructure.DTO
{
    /// <summary>
    /// Priority of the message
    /// </summary>
    public enum FcmPriority
    {
        [JsonProperty("normal")]
        Normal,
        [JsonProperty("high")]
        High
    }
}
