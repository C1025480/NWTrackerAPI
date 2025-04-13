using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors;


namespace TestingProj.Processor
{
    public class DeleteTrackerRecordTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task DeleteProjectTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockTracker = fixture.Create<TT_TRACKER>();

                var TrackerID = 4;

                mockContext.Add(mockTracker);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<DeleteTrackerRecord>();
                var result = sut.delete(mockContext, TrackerID);

                Assert.False(result);
            }
        }
    }
}