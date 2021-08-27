namespace ERService.Infrastructure.Base.Common
{
    public interface IConfig
    {
        DatabaseProvider DatabaseProvider { get; set; }
        string Server { get; set; }
        string User { get; set; }
        string Password { get; set; }
        string LastLogin { get; set; }

        void SaveConfig();
    }
}