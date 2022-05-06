namespace ModernApi.Jobs.FileCleanup
{
    using MediatR;

    public class FileCleanup : IRequest
    {
        public FileCleanupConfig Config { get; set; }
    }
}
