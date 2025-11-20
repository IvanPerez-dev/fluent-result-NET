namespace Osom.FluentRestult.Application.Features.Users.Commands.CreateUser
{
    public record struct CreateUserResponse(
        int Id,
        string FirstName,
        string LastName,
        string Email
    );
}
