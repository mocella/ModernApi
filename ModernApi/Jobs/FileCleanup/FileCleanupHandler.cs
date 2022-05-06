namespace ModernApi.Jobs.FileCleanup;

using System.IO.Abstractions;
using global::Api.Core.Services;
using MediatR;

public class FileCleanupHandler : IRequestHandler<FileCleanup>
{
    private readonly IFileSystem _fileSystem;
    private readonly DateTimeProvider _dateTimeProvider;

    public FileCleanupHandler(DateTimeProvider dateTimeProvider)
        : this(new FileSystem(), dateTimeProvider)
    {
    }

    public FileCleanupHandler(IFileSystem fileSystem, DateTimeProvider dateTimeProvider)
    {
        _fileSystem = fileSystem;
        _dateTimeProvider = dateTimeProvider;
    }

    public Task<Unit> Handle(FileCleanup request, CancellationToken cancellationToken)
    {
        _fileSystem.Directory.GetFiles(request.Config.RootPath)
            .Select(f => _fileSystem.FileInfo.FromFileName(f))
            .Where(f => f.CreationTime <= _dateTimeProvider.OffsetNow.AddDays(-1 * request.Config.RetentionDays))
            .ToList()
            .ForEach(f => f.Delete());

        return Task.FromResult(Unit.Value);
    }
}