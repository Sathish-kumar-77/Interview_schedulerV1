using System.Threading;
using System.Threading.Tasks;
using Assignment.Contracts.Data;
using Assignment.Core.Exceptions;
using MediatR;

public class DeleteUserCommand : IRequest<bool>
{
    public int UserId { get; }

    public DeleteUserCommand(int userId)
    {
        UserId = userId;
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUnitOfWork _repository;

    public DeleteUserCommandHandler(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = _repository.Users.Get(request.UserId);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {request.UserId} not found.");
        }

        _repository.Users.Delete(request.UserId);
        await _repository.CommitAsync();

        return true;
    }
}
