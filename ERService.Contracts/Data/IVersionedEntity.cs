namespace ERService.Contracts.Data
{
    public interface IVersionedEntity
    {
        long RowVersion { get; set; }
    }
}
