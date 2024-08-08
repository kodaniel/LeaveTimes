namespace LeaveTimes.Application.Commands;

public class UpdateLeaveTimeCommand
{
    #region Request DTO

    public record Request(Guid Id, UpdateLeaveTimeDto Item) : IRequest<LeaveTimeDto>;

    #endregion

    #region DTO Validator

    public class Validator : MyValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Item.EmployeeName)
                .NotEmpty()
                    .WithMessage("The name of employee is required.")
                .MaximumLength(100)
                    .WithMessage("The name of the employee can be maximum of 100 characters.");

            RuleFor(x => x.Item.Reason)
                .NotEmpty()
                    .WithMessage("The reason can not be null.")
                .IsEnumName(typeof(Reason), caseSensitive: false)
                    .WithMessage("Reason must be one of the following: 'Holiday', 'PaidLeave', 'NonPaidLeave', 'BusinessTravel', 'HomeOffice'.");

            RuleFor(x => x.Item.StartDate)
                .NotNull()
                    .WithMessage("The start date can not be null.")
                .LessThanOrEqualTo(x => x.Item.EndDate)
                    .WithMessage("The start date must be less than or equal to the end date.");

            RuleFor(x => x.Item.EndDate)
                .NotNull()
                    .WithMessage("The end date can not be null.");

            RuleFor(x => x.Item.Comment)
                .MaximumLength(500)
                    .WithMessage("The comment can be maximum of 100 characters.");
        }
    }

    #endregion

    #region Request handler

    internal class Handler : IRequestHandler<Request, LeaveTimeDto>
    {
        private readonly ILeaveTimeRepository _repository;

        public Handler(ILeaveTimeRepository repository) => _repository = repository;

        public async Task<LeaveTimeDto> Handle(Request request, CancellationToken cancellationToken)
        {
            var leaveTime = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (leaveTime == null)
                throw new NotFoundException(request.Id);

            leaveTime.UpdateName(request.Item.EmployeeName);
            leaveTime.UpdateReason(Enum.Parse<Reason>(request.Item.Reason!, true));
            leaveTime.UpdateTimes(request.Item.StartDate, request.Item.EndDate);
            leaveTime.UpdateComment(request.Item.Comment);

            await _repository.UpdateAsync(leaveTime, cancellationToken);

            return leaveTime.Adapt<LeaveTimeDto>();
        }
    }

    #endregion
}
