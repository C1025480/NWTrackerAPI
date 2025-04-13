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
    public class UpdateTrackerPasswordTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task UpdateTrackerPasswordTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockTracker = fixture.Create<TT_TRACKER>();

                TT_TRACKER mockLOG_Login = new TT_TRACKER();

                var mockvalidatorresult = true;

                mockContext.Add(mockTracker);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<UpdateTrackerRecord>();
                var result = sut.update(mockContext, mockLOG_Login);

                Assert.False(result);
            }
        }
    }
}