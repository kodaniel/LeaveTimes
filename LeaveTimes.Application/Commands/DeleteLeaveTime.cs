namespace LeaveTimes.Application.Commands;

public class DeleteLeaveTime
{
    #region Request DTO

    public class Command(Guid id) : IRequest
    {
        public Guid Id { get; set; } = id;
    }

    #endregion

    #region Request handler

    internal class Handler : IRequestHandler<Command>
    {
        private readonly ILeaveTimeRepository _repository;

        public Handler(ILeaveTimeRepository repository) => _repository = repository;

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var leaveTime = await _repository.GetByIdAsync(command.Id, cancellationToken);

            if (leaveTime == null)
                throw new NotFoundException(command.Id);

            if (leaveTime.IsApproved)
                throw new ApiException("Can not remove approved leave times.", System.Net.HttpStatusCode.BadRequest);

            await _repository.DeleteAsync(leaveTime, cancellationToken);
        }
    }

    #endregion
}
