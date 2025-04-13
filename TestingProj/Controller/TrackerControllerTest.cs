using Moq.AutoMock;
using NWTrackerAPI;
using NWTrackerAPI.Controllers;
using Moq;
using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors.Interfaces;


namespace TestingProj.Controller
{
    public class TrackerControllerTests
    {
        
        private Fixture fixture = new Fixture();

        [Fact]
        public void TrackerControllerGetTrackerRecordsTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                mockContext.Add(mockLogin);
                mockContext.SaveChanges();

                var mockintKey = 4;

                List<TrackerRecord> ReturnedList = new List<TrackerRecord>();

                mock.Mock<IGetTrackerRecords>()
                    .Setup(x => x.get(mockContext, mockintKey))
                    .Returns(ReturnedList);

                var sut = mock.Create<TrackerController>();
                var result = sut.GetTrackerRecords(mockintKey);

                Assert.NotNull(result);
            }
        }
        [Fact]
        public void TrackerControllerGetTrackerRecordTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                mockContext.Add(mockLogin);
                mockContext.SaveChanges();

                var mockintKey = 4;

                TT_TRACKER TestRecord = new TT_TRACKER()
                {
                    TT_NW_FK = 2
                };

                mock.Mock<IGetTrackerRecord>()
                    .Setup(x => x.get(mockContext, mockintKey))
                    .Returns(TestRecord);

                var sut = mock.Create<TrackerController>();
                var result = sut.GetTrackerRecord(mockintKey);

                Assert.NotNull(result);
            }
        }
        [Fact]
        public async Task TrackerControllerDeleteTrackerRecordTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                mockContext.Add(mockLogin);
                await mockContext.SaveChangesAsync();

                var mockintKey = 4;

                bool deletesuccess = true;

                mock.Mock<IDeleteTrackerRecord>()
                    .Setup(x => x.delete(mockContext, mockintKey))
                    .Returns(deletesuccess);

                var sut = mock.Create<TrackerController>();
                var result = sut.DeleteTrackerRecord(mockintKey);

                Assert.NotNull(result);
            }
        }
        [Fact]
        public void TrackerControllerUpdateTrackerRecordTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                mockContext.Add(mockLogin);
                mockContext.SaveChanges();

                var mockintKey = 4;

                bool deletesuccess = true;

                TT_TRACKER TestRecord = new TT_TRACKER()
                {
                    TT_NW_FK = 2
                };

                mock.Mock<IUpdateTrackerRecord>()
                    .Setup(x => x.update(mockContext, TestRecord))
                    .Returns(deletesuccess);

                var sut = mock.Create<TrackerController>();
                var result = sut.UpdateTrackerRecord(TestRecord);

                Assert.NotNull(result);
            }
        }
    }
}