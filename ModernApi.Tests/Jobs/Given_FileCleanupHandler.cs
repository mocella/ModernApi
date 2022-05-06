namespace ModernApi.Tests.Jobs;

using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using global::Api.Core.Services;
using ModernApi.Jobs.FileCleanup;
using Moq;
using Xunit;

public class Given_FileCleanupHandler
{
    public const string TestOffsetNowDate = "5/6/2022 12:00:00 PM -05:00";


    [Theory]
    [InlineData("3/27/2022 12:00:00 PM -05:00", true)]
    [InlineData("4/6/2022 12:00:00 PM -05:00", true)]
    [InlineData("4/7/2022 12:00:00 PM -05:00", false)]
    public async Task Should_Remove_Files_Older_Than_Retention_Days(DateTimeOffset fileCreationTime, bool expectedResult)
    {
        // arrange
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
        {
            {
                @"c:\temp\myFile.txt", new MockFileData("") { CreationTime = fileCreationTime }
            }
        });
        var mockDateTimeProvider = new Mock<DateTimeProvider>();
        mockDateTimeProvider.Setup(dtp => dtp.OffsetNow).Returns(DateTimeOffset.Parse(TestOffsetNowDate));

        var handler = new FileCleanupHandler(fileSystem, mockDateTimeProvider.Object);
        var request = new FileCleanup
        {
            Config = new FileCleanupConfig
            {
                RetentionDays = 30,
                RootPath = @"c:\temp\"
            }
        };

        // act
        await handler.Handle(request, CancellationToken.None);

        // assert
        Assert.Equal(expectedResult, !fileSystem.AllFiles.Any());
    }
}