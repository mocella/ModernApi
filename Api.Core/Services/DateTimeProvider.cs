namespace Api.Core.Services;

using System;

public class DateTimeProvider
{
    public virtual DateTime Now => DateTime.Now;
    public virtual DateTime UtcNow => DateTime.UtcNow;
    public virtual DateTimeOffset OffsetNow => DateTimeOffset.Now;
    public virtual DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;
}