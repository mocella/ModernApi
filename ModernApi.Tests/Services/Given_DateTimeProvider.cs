using System;
using System.Threading;
using Api.Core.Services;
using Xunit;

namespace ModernApi.Tests.Services;

[Trait(Constants.TestCategory, Constants.UnitTestCategory)]
public class Given_DateTimeProvider
{
    [Fact]
    public void Should_Get_Current_DateTime_Each_Time()
    {
        // arrange
        var dateTimeProvider = new DateTimeProvider();
        var sleepDelayMs = 50;
        DateTime firstDate, secondDate;
        DateTimeOffset firstOffset, secondOffset;
        
        // act 
        firstDate = dateTimeProvider.Now;
        firstOffset = new DateTimeOffset(firstDate);
        Thread.Sleep(sleepDelayMs);
        secondDate = dateTimeProvider.Now;
        secondOffset = new DateTimeOffset(secondDate);

        // assert
        Assert.True(secondOffset.ToUnixTimeMilliseconds() > firstOffset.ToUnixTimeMilliseconds());
    }
}