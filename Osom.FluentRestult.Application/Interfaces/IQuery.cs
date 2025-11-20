using FluentResults;
using MediatR;

namespace Osom.FluentRestult.Application.Interfaces
{
    internal interface IQuery<T> : IRequest<Result<T>> { }
}
