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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TestingProj.Controller
{
    public class ProjectControllerTests
    {

        private Fixture fixture = new Fixture();

        [Fact]
        public async Task ProjectControllerGetProjectsTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockProject = fixture.Create<Project>();

                await mockContext.AddAsync(mockProject);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<ProjectController>();
                var result = sut.GetProjects();

                Assert.IsType<List<Project>>(result.Value);

                var projects = result.Value as List<Project>;

                var project = projects!.First();

                Assert.Equal(mockProject.NW_PK, mockProject.NW_PK);

                Assert.NotNull(result);
            }
        }
        [Fact]
        public async Task ProjectControllerNewProjectTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockProject = fixture.Create<Project>();
                IActionResult ExpectedResult = new OkResult();

                bool createProjectResult = true;

                mock.Mock<ICreateProject>()
                    .Setup(x => x.create( mockContext, mockProject))
                    .Returns(createProjectResult);

                mockContext.Add(mockProject);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<ProjectController>();
                var result = sut.NewProject(mockProject);

                var okResult = Assert.IsType<OkResult>(result);
                Assert.Equal(200, okResult.StatusCode);

                Assert.NotNull(result);
            }
        }
        [Fact]
        public async Task ProjectControllerDeleteProjectTest()
        {
            var options = new DbContextOptionsBuilder<APIContext>().UseInMemoryDatabase("testDB").Options;
            APIContext mockContext = new APIContext(options);

            using (var mock = AutoMock.GetLoose(x => x.RegisterInstance(mockContext).As<APIContext>()))
            {
                var mockProject = fixture.Create<Project>();

                bool createProjectResult = true;

                mock.Mock<IDeleteProject>()
                    .Setup(x => x.delete(mockContext, mockProject))
                    .Returns(createProjectResult);

                mockContext.Add(mockProject);
                await mockContext.SaveChangesAsync();

                var sut = mock.Create<ProjectController>();
                var result = sut.DeleteProject(mockProject);

                var okResult = Assert.IsType<OkResult>(result);
                Assert.Equal(200, okResult.StatusCode);

                Assert.NotNull(result);
            }
        }
    }
}