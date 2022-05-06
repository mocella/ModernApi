namespace Api.Core.Services;

using System;

public class DateTimeProvider
{
    public virtual DateTime Now { get; } = DateTime.Now;
    public virtual DateTime UtcNow { get; } = DateTime.UtcNow;
    public virtual DateTimeOffset OffsetNow { get; } = DateTimeOffset.Now;
    public virtual DateTimeOffset OffsetUtcNow { get; } = DateTimeOffset.UtcNow;
}