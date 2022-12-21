namespace Infrastructure.Persistence.Options
{
    public class DbOptions
    {
        public const string Key = nameof(DbOptions);
        public string ConnectionString { get; set; } = null!;
    }
}
