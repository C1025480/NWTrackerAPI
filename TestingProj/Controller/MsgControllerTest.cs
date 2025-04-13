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
    public class MsgControllerTests
    {
        
        private Fixture fixture = new Fixture();

        [Fact]
        public void MsgControllerGetMsgsTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                var mockMsgRecipientValue = 4;

                LOG_LOGIN mockLOG_Login = new LOG_LOGIN();

                mockContext.Add(mockLogin);
                mockContext.SaveChanges();

                mock.Mock<IGetUser>()
                    .Setup(x => x.GetUserFromLogId(mockContext, mockMsgRecipientValue))
                    .Returns(mockLOG_Login);

                var sut = mock.Create<MsgController>();
                var result = sut.GetTrackerRecords(mockMsgRecipientValue);

                Assert.NotNull(result);
            }
        }
    }
}