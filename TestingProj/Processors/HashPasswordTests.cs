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
    public class HashPasswordTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task HashPasswordTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockTracker = fixture.Create<TT_TRACKER>();

                var mockHash = "testUser";

                mockContext.Add(mockTracker);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<HashPassword>();
                var result = sut.PasswordHasher(mockHash);

                Assert.NotNull(result);
            }
        }
    }
}