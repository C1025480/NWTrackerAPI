using NWTrackerAPI.Controllers;
using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors.Interfaces;


namespace TestingProj.Controller
{
    public class UserControllerTests
    {
        
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task UserControllerGetLoginTestSuccess()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                LOG_LOGIN mockLOG_Login = new LOG_LOGIN();

                await mockContext.AddAsync(mockLogin);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<UserController>();
                var result = sut.GetLogin();

                Assert.NotNull(result);
            }
        }
        [Fact]
        public void UserControllerCreateUserTestSuccess()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                LOG_LOGIN mockLOG_Login = new LOG_LOGIN();

                mockContext.Add(mockLogin);
                mockContext.SaveChanges();

                mock.Mock<ICreateUser>()
                    .Setup(x => x.create(mockContext, mockLogin));

                var sut = mock.Create<UserController>();
                var result = sut.CreateUser(mockLogin);

                Assert.NotNull(result);
            }
        }
        [Fact]
        public void UserControllerUserLoginTestSuccess()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<LOG_LOGIN>();

                LOG_LOGIN mockLOG_Login = new LOG_LOGIN();

                mockContext.Add(mockLogin);
                mockContext.SaveChanges();

                LoginModel testLoginModel = new LoginModel();

                mock.Mock<IGetUser>()
                    .Setup(x => x.Validate(mockContext, testLoginModel.username, testLoginModel.password));

                var sut = mock.Create<UserController>();
                var result = sut.UserLogin(testLoginModel);

                Assert.NotNull(result);
            }
        }
    }
}