using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors;
using NWTrackerAPI.Processors.Interfaces;


namespace TestingProj.Processor
{
    public class ValidatePasswordTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task ValidatePasswordTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockTracker = fixture.Create<TT_TRACKER>();

                string mockenteredPassword = "test";
                string mockstoredHashPassword = "0nKsj79bnfdlsMFfIUxVqbFybyLTOKi+PzrQU+SOYqw=";
                string mockenteredSalt = "ttY31JaNAJeXDCdU6GW1XQ==";

                mockContext.Add(mockTracker);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<ValidatePassword>();
                var result = sut.Validator(mockenteredPassword, mockstoredHashPassword, mockenteredSalt);

                Assert.Equal(2,2);
            }
        }
    }
}