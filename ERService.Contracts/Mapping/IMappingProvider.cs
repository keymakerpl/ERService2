namespace ERService.Contracts.Mapping
{
    public interface IMappingProvider
    {
        TDestination MapTo<TDestination>(object source);
    }
}