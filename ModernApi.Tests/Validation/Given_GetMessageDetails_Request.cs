namespace ModernApi.Tests.Validation
{
    using System;
    using Model;
    using ModernApi.Validation;
    using Xunit;

    [Trait(Constants.TestCategory, Constants.UnitTestCategory)]
    public class Given_GetMessageDetails_Request
    {
        private readonly GetMessageDetailsValidator _validator;

        public Given_GetMessageDetails_Request()
        {
            _validator = new GetMessageDetailsValidator();
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", false)]
        [InlineData("BBDBDD45-1424-4E95-943E-19479E2DC77D", true)]

        public void Should_Require_ValidGuid(Guid messageGuid, bool expectedResult)
        {
            // arrange
            var request = new GetMessageDetails(messageGuid);

            // act 
            var result = _validator.Validate(request);

            // assert
            Assert.Equal(expectedResult, result.IsValid);
        }
    }
}
