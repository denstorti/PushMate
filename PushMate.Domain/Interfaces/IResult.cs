namespace PushMate.Domain.Interfaces
{
    public interface IResult
    {
        string Error { get; set; }
        string MessageId { get; set; }
        string RegistrationId { get; set; }
    }
}