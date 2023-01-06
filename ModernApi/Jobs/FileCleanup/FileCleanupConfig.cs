namespace ModernApi.Jobs.FileCleanup;

public class FileCleanupConfig
{
    public string? CronExpression { get; set; }

    public bool InstantRun { get; set; }
    public string? RootPath { get; set; }
    public int RetentionDays { get; set; }
}