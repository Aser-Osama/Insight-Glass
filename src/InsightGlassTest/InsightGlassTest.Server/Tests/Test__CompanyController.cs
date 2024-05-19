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
    public class CompaniesControllerTests
    {
        private DbContextOptions<idbcontext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<idbcontext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetCompanies_ReturnsAllCompanies()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var users = new List<Aspnetuser>
            {
                new Aspnetuser { Id = "1" },
                new Aspnetuser { Id = "2" }
            };

            var companies = new List<Company>
            {
                new Company { CompanyUserId = "1", CompanyCity = "City1", CompanyIndustry = "Industry1", CompanyUser = users[0] },
                new Company { CompanyUserId = "2", CompanyCity = "City2", CompanyIndustry = "Industry2", CompanyUser = users[1] }
            };

            context.Aspnetusers.AddRange(users);
            context.Companies.AddRange(companies);
            context.SaveChanges();

            var controller = new CompaniesController(context);

            // Act
            var result = await controller.GetCompanies();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Company>>>(result);
            var returnValue = Assert.IsType<List<Company>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCompany_ReturnsCompany_WhenCompanyExists()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var users = new List<Aspnetuser>
            {
                new Aspnetuser { Id = "1" },
                new Aspnetuser { Id = "2" }
            };

            var companies = new List<Company>
            {
                new Company { CompanyUserId = "1", CompanyCity = "City1", CompanyIndustry = "Industry1", CompanyUser = users[0] },
                new Company { CompanyUserId = "2", CompanyCity = "City2", CompanyIndustry = "Industry2", CompanyUser = users[1] }
            };

            context.Aspnetusers.AddRange(users);
            context.Companies.AddRange(companies);
            context.SaveChanges();

            var controller = new CompaniesController(context);

            // Act
            var result = await controller.GetCompany("1");

            // Assert
            var actionResult = Assert.IsType<ActionResult<Company>>(result);
            var returnValue = Assert.IsType<Company>(actionResult.Value);
            Assert.Equal("1", returnValue.CompanyUserId);
            Assert.Equal("City1", returnValue.CompanyCity);
        }

        [Fact]
        public async Task GetCompany_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var users = new List<Aspnetuser>
            {
                new Aspnetuser { Id = "1" },
                new Aspnetuser { Id = "2" }
            };

            var companies = new List<Company>
            {
                new Company { CompanyUserId = "1", CompanyCity = "City1", CompanyIndustry = "Industry1", CompanyUser = users[0] },
                new Company { CompanyUserId = "2", CompanyCity = "City2", CompanyIndustry = "Industry2", CompanyUser = users[1] }
            };

            context.Aspnetusers.AddRange(users);
            context.Companies.AddRange(companies);
            context.SaveChanges();

            var controller = new CompaniesController(context);

            // Act
            var result = await controller.GetCompany("3");

            // Assert
            var actionResult = Assert.IsType<ActionResult<Company>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task AddCompany_AddsCompany_WhenValidCompanyIsProvided()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var user = new Aspnetuser { Id = "3" };
            var company = new Company { CompanyUserId = "3", CompanyCity = "City3", CompanyIndustry = "Industry3", CompanyUser = user };

            context.Aspnetusers.Add(user);
            context.SaveChanges();

            var controller = new CompaniesController(context);

            // Act
            var result = await controller.PostCompany(company);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Company>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Company>(createdAtActionResult.Value);
            Assert.Equal("3", returnValue.CompanyUserId);
            Assert.Equal("City3", returnValue.CompanyCity);

            var companies = context.Companies.ToList();
            Assert.Single(companies);
            Assert.Equal("3", companies[0].CompanyUserId);
        }

        [Fact]
        public async Task DeleteCompany_DeletesCompany_WhenCompanyExists()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var user = new Aspnetuser { Id = "4" };
            var company = new Company { CompanyUserId = "4", CompanyCity = "City4", CompanyIndustry = "Industry4", CompanyUser = user };

            context.Aspnetusers.Add(user);
            context.Companies.Add(company);
            context.SaveChanges();

            var controller = new CompaniesController(context);

            // Act
            var result = await controller.DeleteCompany("4");

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);

            var companies = context.Companies.ToList();
            Assert.Empty(companies);
        }

        [Fact]
        public async Task DeleteCompany_ReturnsNotFound_WhenCompanyDoesNotExist()
        {
            // Arrange
            var options = CreateNewContextOptions();
            await using var context = new idbcontext(options);

            var controller = new CompaniesController(context);

            // Act
            var result = await controller.DeleteCompany("5");

            // Assert
            var actionResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
