namespace LeaveTimes.Application.Queries;

public class GetAllLeaveTimesQuery
{
    #region Request DTO

    public record Request(int? year, int? month, string? employeeName, string? reason) : IRequest<ListResponse<LeaveTimeDto>>;

    #endregion

    #region DTO Validator

    public class Validator : MyValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.reason)
                .IsEnumName(typeof(Reason), caseSensitive: false)
                .When(x => !string.IsNullOrEmpty(x.reason))
                .WithMessage("Reason must be one of the following: 'Holiday', 'PaidLeave', 'NonPaidLeave', 'BusinessTravel', 'HomeOffice'.");

            RuleFor(x => x.year)
                .InclusiveBetween(2000, 2100)
                .When(x => x.year.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Year must be between 2000 and 2100");

            RuleFor(x => x.month)
                .InclusiveBetween(1, 12)
                .When(x => x.month.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Month must be between 1 and 12");
        }
    }

    #endregion

    #region Request handler

    internal class Handler : IRequestHandler<Request, ListResponse<LeaveTimeDto>>
    {
        private readonly ILeaveTimeRepository _repository;

        public Handler(ILeaveTimeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ListResponse<LeaveTimeDto>> Handle(Request request, CancellationToken cancellationToken)
        {
            int year = request.year ?? DateTime.Now.Year;
            int month = request.month ?? DateTime.Now.Month;
            Reason? reason = null;
            if (Enum.TryParse(request.reason, ignoreCase: true, out Reason r))
                reason = r;

            var leaveTimes = await _repository.FilteredListAsync(year, month, request.employeeName, reason, cancellationToken);

            var leaveTimeDtos = leaveTimes.Adapt<IEnumerable<LeaveTimeDto>>();
            return new ListResponse<LeaveTimeDto>(leaveTimeDtos);
        }
    }

    #endregion
}
