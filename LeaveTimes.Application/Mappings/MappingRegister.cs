namespace LeaveTimes.Application.Mappings;

internal class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // Domain object -> Response DTO
        config.ForType<LeaveTime, LeaveTimeDto>();
    }
}
