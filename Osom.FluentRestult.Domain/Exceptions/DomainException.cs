using Osom.FluentRestult.Domain.Interfaces;

namespace Osom.FluentRestult.Domain.Exceptions
{
    public abstract class DomainException : Exception, ICustomException
    {
        public abstract string Title { get; }
        public abstract int StatusCode { get; }
        public abstract string Type { get; }
        public virtual string Detail => Message;

        protected DomainException(string message)
            : base(message) { }
    }
}
