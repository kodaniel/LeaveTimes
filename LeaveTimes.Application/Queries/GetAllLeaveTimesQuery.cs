namespace LeaveTimes.Application.Queries;

public class GetAllLeaveTimesQuery
{
    #region Request DTO

    public class Request : IRequest<ListResponse<LeaveTimeDto>>
    {
        /// <summary>
        /// Year, leave it empty for current year.
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// Month, leave it empty for current month.
        /// </summary>
        public int? Month { get; set; }

        /// <summary>
        /// Search for the employee name.
        /// </summary>
        public string? EmployeeName { get; set; }

        /// <summary>
        /// Filter the reasons.
        /// </summary>
        public string? Reason { get; set; }
    }

    #endregion

    #region DTO Validator

    public class Validator : MyValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Reason)
                .IsEnumName(typeof(Reason), caseSensitive: false)
                .When(x => !string.IsNullOrEmpty(x.Reason))
                .WithMessage("Reason must be one of the following: 'Holiday', 'PaidLeave', 'NonPaidLeave', 'BusinessTravel', 'HomeOffice'.");

            RuleFor(x => x.Year)
                .InclusiveBetween(2000, 2100)
                .When(x => x.Year.HasValue, ApplyConditionTo.CurrentValidator)
                .WithMessage("Year must be between 2000 and 2100");

            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12)
                .When(x => x.Month.HasValue, ApplyConditionTo.CurrentValidator)
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
            int year = request.Year ?? DateTime.Now.Year;
            int month = request.Month ?? DateTime.Now.Month;
            Reason? reason = null;
            if (Enum.TryParse(request.Reason, ignoreCase: true, out Reason r))
                reason = r;

            var leaveTimes = await _repository.FilteredListAsync(year, month, request.EmployeeName, reason, cancellationToken);

            var leaveTimeDtos = leaveTimes.Adapt<IEnumerable<LeaveTimeDto>>();
            return new ListResponse<LeaveTimeDto>(leaveTimeDtos);
        }
    }

    #endregion
}
