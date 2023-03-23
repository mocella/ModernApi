namespace Api.Core.Services;

using System;

public class DateTimeProvider
{
    public virtual DateTime Now
    {
        get { return DateTime.Now; }
    }

    public virtual DateTime UtcNow
    {
        get { return DateTime.UtcNow; }
    }

    public virtual DateTimeOffset OffsetNow
    {
        get { return DateTimeOffset.Now; }
    }

    public virtual DateTimeOffset OffsetUtcNow
    {
        get { return DateTimeOffset.UtcNow; }
    }
}