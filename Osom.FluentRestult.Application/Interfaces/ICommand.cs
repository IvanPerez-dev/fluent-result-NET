using FluentResults;
using MediatR;

namespace Osom.FluentRestult.Application.Interfaces
{
    public interface ICommand<T> : IRequest<Result<T>> { }
}
