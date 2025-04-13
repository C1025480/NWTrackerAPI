using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors;


namespace TestingProj.Processor
{
    public class GetTrackerRecordsTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task GetTrackerRecordsTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockTracker = fixture.Create<TT_TRACKER>();

                var ProjectPK = 4;

                mockContext.Add(mockTracker);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<GetTrackerRecord>();
                var result = sut.get(mockContext, ProjectPK);

                Assert.Null(result);
            }
        }
    }
}