namespace ERService.Contracts.Data
{

    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }
}
