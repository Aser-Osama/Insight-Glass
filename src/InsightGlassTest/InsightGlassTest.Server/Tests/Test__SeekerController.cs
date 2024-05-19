using InsightGlassTest.Server.Controllers;
using InsightGlassTest.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InsightGlassTest.Server.Tests
{
    public class SeekersControllerTests
    {
        private DbContextOptions<idbcontext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<idbcontext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetSeekers_ReturnsAllSeekers()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var users = new List<Aspnetuser>
            {
                new Aspnetuser { Id = "1" },
                new Aspnetuser { Id = "2" }
            };

            var seekers = new List<Seeker>
            {
                new Seeker { SeekerUserId = "1", SeekerCity = "City1", SeekerUser = users[0] },
                new Seeker { SeekerUserId = "2", SeekerCity = "City2", SeekerUser = users[1] }
            };

            context.Aspnetusers.AddRange(users);
            context.Seekers.AddRange(seekers);
            context.SaveChanges();

            var controller = new SeekersController(context);

            // Act
            var result = await controller.GetSeekers();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Seeker>>>(result);
            var returnValue = Assert.IsType<List<Seeker>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetSeeker_ReturnsSeeker_WhenSeekerExists()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var users = new List<Aspnetuser>
            {
                new Aspnetuser { Id = "1" },
                new Aspnetuser { Id = "2" }
            };

            var seekers = new List<Seeker>
            {
                new Seeker { SeekerUserId = "1", SeekerCity = "City1", SeekerUser = users[0] },
                new Seeker { SeekerUserId = "2", SeekerCity = "City2", SeekerUser = users[1] }
            };

            context.Aspnetusers.AddRange(users);
            context.Seekers.AddRange(seekers);
            context.SaveChanges();

            var controller = new SeekersController(context);

            // Act
            var result = await controller.GetSeeker("1");

            // Assert
            var actionResult = Assert.IsType<ActionResult<Seeker>>(result);
            var returnValue = Assert.IsType<Seeker>(actionResult.Value);
            Assert.Equal("1", returnValue.SeekerUserId);
            Assert.Equal("City1", returnValue.SeekerCity);
        }

        [Fact]
        public async Task GetSeeker_ReturnsNotFound_WhenSeekerDoesNotExist()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var users = new List<Aspnetuser>
            {
                new Aspnetuser { Id = "1" },
                new Aspnetuser { Id = "2" }
            };

            var seekers = new List<Seeker>
            {
                new Seeker { SeekerUserId = "1", SeekerCity = "City1", SeekerUser = users[0] },
                new Seeker { SeekerUserId = "2", SeekerCity = "City2", SeekerUser = users[1] }
            };

            context.Aspnetusers.AddRange(users);
            context.Seekers.AddRange(seekers);
            context.SaveChanges();

            var controller = new SeekersController(context);

            // Act
            var result = await controller.GetSeeker("3");

            // Assert
            var actionResult = Assert.IsType<ActionResult<Seeker>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddSeeker_AddsSeeker_WhenValidSeekerIsProvided()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var user = new Aspnetuser { Id = "3" };
            var seeker = new Seeker { SeekerUserId = "3", SeekerCity = "City3", SeekerUser = user };

            context.Aspnetusers.Add(user);
            context.SaveChanges();

            var controller = new SeekersController(context);

            // Act
            var result = await controller.PostSeeker(seeker);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Seeker>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Seeker>(createdAtActionResult.Value);
            Assert.Equal("3", returnValue.SeekerUserId);
            Assert.Equal("City3", returnValue.SeekerCity);

            var seekers = context.Seekers.ToList();
            Assert.Single(seekers);
            Assert.Equal("3", seekers[0].SeekerUserId);
        }

        [Fact]
        public async Task DeleteSeeker_DeletesSeeker_WhenSeekerExists()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var user = new Aspnetuser { Id = "4" };
            var seeker = new Seeker { SeekerUserId = "4", SeekerCity = "City4", SeekerUser = user };

            context.Aspnetusers.Add(user);
            context.Seekers.Add(seeker);
            context.SaveChanges();

            var controller = new SeekersController(context);

            // Act
            var result = await controller.DeleteSeeker("4");

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);

            var seekers = context.Seekers.ToList();
            Assert.Empty(seekers);
        }

        [Fact]
        public async Task DeleteSeeker_ReturnsNotFound_WhenSeekerDoesNotExist()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var controller = new SeekersController(context);

            // Act
            var result = await controller.DeleteSeeker("5");

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
