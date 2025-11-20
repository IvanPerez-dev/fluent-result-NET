namespace Osom.FluentRestult.Domain.Interfaces
{
    public interface ICustomException
    {
        string Title { get; }
        int StatusCode { get; }
        string Detail { get; }
        string Type { get; }
    }
}
