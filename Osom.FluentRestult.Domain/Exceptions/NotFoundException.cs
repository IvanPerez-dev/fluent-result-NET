namespace Osom.FluentRestult.Domain.Exceptions
{
    public class NotFoundException : DomainException
    {
        public override string Title => throw new NotImplementedException();

        public override int StatusCode => throw new NotImplementedException();

        public override string Type => throw new NotImplementedException();

        public NotFoundException(string message)
            : base(message) { }
    }
}
