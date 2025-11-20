using Osom.FluentRestult.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Osom.FluentRestult.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public string Title => "Validation error";
        public int StatusCode => StatusCodes.Status400BadRequest;
        public string Type => "https://tools.ietf.org/html/rfc7231#section-6.5.1";

        public Dictionary<string, List<string>> Failures { get; } = new();

        public ValidationException(string message)
            : base(message) { }

        public ValidationException(Dictionary<string, List<string>> failures)
            : base("One or more fields are invalid.")
        {
            Failures = new();
            foreach (var failure in failures)
            {
                if (Failures.ContainsKey(failure.Key))
                {
                    Failures[failure.Key].AddRange(failure.Value);
                }
                else
                {
                    Failures.Add(failure.Key, failure.Value);
                }
            }
        }

        public ValidationException(IEnumerable<KeyValuePair<string, List<string>>> failures)
            : base("One or more fields are invalid.")
        {
            Failures = new();
            foreach (var failure in failures)
            {
                if (Failures.ContainsKey(failure.Key))
                {
                    Failures[failure.Key].AddRange(failure.Value);
                }
                else
                {
                    Failures.Add(failure.Key, failure.Value);
                }
            }
        }
    }
}
