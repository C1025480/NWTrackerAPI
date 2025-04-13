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
    public class GetUserTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task GetUserTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockTracker = fixture.Create<TT_TRACKER>();

                var mockUsername = "testUser";
                var mockPassword = "testPassword";
                var mockHash = "testesttesttest";
                var mockSalt = "testtesttest123";
                var mockvalidatorresult = true;

                mock.Mock<IValidatePassword>()
                    .Setup(x => x.Validator(mockPassword, mockHash, mockSalt))
                    .Returns(mockvalidatorresult);

                mockContext.Add(mockTracker);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<GetUser>();
                var result = sut.Validate(mockContext, mockUsername, mockPassword);

                Assert.Null(result);
            }
        }
        [Fact]
        public async Task GetUserTestForGetUsers()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockTracker = fixture.Create<TT_TRACKER>();

                var mockLogId = 4;

                mockContext.Add(mockTracker);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<GetUser>();
                var result = sut.GetUserFromLogId(mockContext, mockLogId);

                Assert.Null(result);
            }
        }
    }
}