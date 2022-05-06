namespace ModernApi.Jobs.FileCleanup;

using global::Api.Core.Services;
using MediatR;
using Microsoft.Extensions.Options;

public class FileCleanupJob : CronJobService
{
    private readonly FileCleanupConfig _fileCleanupConfig;
    private readonly IMediator _mediator;

    public FileCleanupJob(IOptions<FileCleanupConfig> settings, IMediator mediator)
        : base(settings.Value.CronExpression, settings.Value.InstantRun)
    {
        _fileCleanupConfig = settings.Value;
        _mediator = mediator;
    }

    protected override async Task DoWork(CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new FileCleanup { Config = _fileCleanupConfig },
            cancellationToken
        );
    }
}