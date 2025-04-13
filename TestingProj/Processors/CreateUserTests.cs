using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors;


namespace TestingProj.Processor
{
    public class CreateUserTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task CreateUserTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                LOG_LOGIN mockLoginVar = new LOG_LOGIN()
                {
                    LOG_ID = 1,
                    LOG_EMAILADDRESS ="Test",
                    LOG_FIRSTNAME ="test",
                    LOG_PHONENUMBER ="test",
                    LOG_SECONDNAME ="test",
                    LOG_USERNAME ="test"
                };

                mockContext.Add(mockLogin);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<CreateUser>();
                var result = sut.create(mockContext, mockLoginVar);

                Assert.True(result);
            }
        }
    }
}