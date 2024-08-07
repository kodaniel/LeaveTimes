namespace LeaveTimes.Application.Commands;

public class CreateLeaveTimeCommand
{
    #region Request DTO

    public record Request(string? employeeName, DateTime? startDate, DateTime? endDate, string? reason, string? comment) : IRequest<LeaveTimeDto>;

    #endregion

    #region DTO Validator

    public class Validator : MyValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.employeeName)
                .NotEmpty()
                    .WithMessage("The name of employee is required.")
                .MaximumLength(100)
                    .WithMessage("The name of the employee can be maximum of 100 characters.");

            RuleFor(x => x.reason)
                .NotEmpty()
                    .WithMessage("The reason can not be null.")
                .IsEnumName(typeof(Reason), caseSensitive: false)
                    .WithMessage("Reason must be one of the following: 'Holiday', 'PaidLeave', 'NonPaidLeave', 'BusinessTravel', 'HomeOffice'.");

            RuleFor(x => x.startDate)
                .NotNull()
                    .WithMessage("The start date can not be null.")
                .LessThanOrEqualTo(x => x.endDate)
                    .WithMessage("The start date must be less than or equal to the end date.");

            RuleFor(x => x.endDate)
                .NotNull()
                    .WithMessage("The end date can not be null.");

            RuleFor(x => x.comment)
                .MaximumLength(500)
                    .WithMessage("The comment can be maximum of 100 characters.");
        }
    }

    #endregion

    #region Request handler

    internal class Handler : IRequestHandler<Request, LeaveTimeDto>
    {
        private readonly ILeaveTimeRepository _repository;

        public Handler(ILeaveTimeRepository repository)
        {
            _repository = repository;
        }

        public async Task<LeaveTimeDto> Handle(Request request, CancellationToken cancellationToken)
        {
            var leaveTime = LeaveTime.Create(request.employeeName!);
            leaveTime.UpdateReason(Enum.Parse<Reason>(request.reason!));
            leaveTime.UpdateTimes(request.startDate!.Value, request.endDate!.Value);
            leaveTime.UpdateComment(request.comment!);

            await _repository.AddAsync(leaveTime);

            return leaveTime.Adapt<LeaveTimeDto>();
        }
    }

    #endregion
}
