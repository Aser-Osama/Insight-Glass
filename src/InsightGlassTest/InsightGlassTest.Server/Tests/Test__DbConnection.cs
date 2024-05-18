using InsightGlassTest.Server.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InsightGlassTest.Server.Tests
{
    public class DatabaseTests
    {
        private readonly DbContextOptions<idbcontext> _options;
        private readonly string _connectionString;


        public DatabaseTests()
        {
            _connectionString = Environment.GetEnvironmentVariable("MYSQLCONNSTR_DBLiveConn");
            Assert.NotNull(_connectionString);

            _options = new DbContextOptionsBuilder<idbcontext>()
                .UseMySql(_connectionString, new MySqlServerVersion(new Version(8, 0, 34)))
                .Options;
        }

        [Fact]
        public void Test_ConnectionWithEntityFramework()
        {
            using (var context = new idbcontext(_options))
            {
                var canConnect = context.Database.CanConnect();
                Assert.True(canConnect);
            }
        }

        [Fact]
        public void Test_GetRandomCompany()
        {
            // Arrange
            using (var context = new idbcontext(_options))
            {
                var company = context.Companies.FirstOrDefault();
                Assert.NotNull(company);
            }
        }
    }
}
