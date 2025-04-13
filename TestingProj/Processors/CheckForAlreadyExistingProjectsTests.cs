using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors;


namespace TestingProj.Processor
{
    public class CheckForAlreadyExistingProjectTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task CheckForAlreadyExistingProjectsTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<Project>();

                Project mockProject = new Project();

                mockContext.Add(mockLogin);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<CheckForAlreadyExistingProjects>();
                var result = sut.check(mockContext, mockProject);

                Assert.False(result);
            }
        }
    }
}