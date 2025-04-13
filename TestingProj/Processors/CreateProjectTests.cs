using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors;


namespace TestingProj.Processor
{
    public class CreateProjectTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task CreateProjectTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockLogin = fixture.Create<Project>();

                Project mockProject = new Project()
                {
                    NW_PK = 1,
                    ProjectName = "Test"
                };

                mockContext.Add(mockLogin);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<CreateProject>();
                var result = sut.create(mockContext, mockProject);

                Assert.True(result);
            }
        }
    }
}