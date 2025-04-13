using Autofac;
using Autofac.Extras.Moq;
using NWTrackerAPI.Data;
using Microsoft.EntityFrameworkCore;
using NWTrackerAPI.Models;
using AutoFixture;
using NWTrackerAPI.Processors;


namespace TestingProj.Processor
{
    public class DeleteProjectTests
    {
        private Fixture fixture = new Fixture();

        [Fact]
        public async Task DeleteProjectTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockProject = fixture.Create<Project>();

                Project mockProjectVar = new Project()
                {
                    NW_PK = 1,
                    ProjectName = "Test"
                };

                mockContext.Add(mockProject);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<DeleteProject>();
                var result = sut.delete(mockContext, mockProjectVar);

                Assert.False(result);
            }
        }
    }
}