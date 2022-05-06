namespace ModernApi.Jobs.FileCleanup
{
    using System.IO.Abstractions;
    using MediatR;

    public class FileCleanupHandler : IRequestHandler<FileCleanup>
    {
        private readonly IFileSystem _fileSystem;

        public FileCleanupHandler() 
            : this(new FileSystem())
        {
        }

        public FileCleanupHandler(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public Task<Unit> Handle(FileCleanup request, CancellationToken cancellationToken)
        {
            _fileSystem.Directory.GetFiles(request.Config.RootPath)
                .Select(f => new FileInfo(f))
                .Where(f => f.LastWriteTime < DateTime.Now.AddDays(-1 * request.Config.RetentionDays))
                .ToList()
                .ForEach(f => f.Delete());
            
            return (Task<Unit>)Task.CompletedTask;
        }
    }
}
