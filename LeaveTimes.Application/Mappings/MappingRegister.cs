namespace LeaveTimes.Application.Mappings;

internal class MappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<LeaveTime, LeaveTimeDto>();
    }
}
