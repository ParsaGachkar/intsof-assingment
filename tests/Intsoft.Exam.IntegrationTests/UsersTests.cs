using Intsof.Exam.EfCore.DbContext;
using Intsoft.Exam.API.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace Intsoft.Exam.IntegrationTests;

public class UsersTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public UsersTests(WebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Fact]
    public async Task Create_User()
    {
        //Arrange
        var client = _webApplicationFactory.CreateClient();
        //Act
        var httpResult = await client.PostAsJsonAsync("/api/v1/users",
            new CreateUserDto()
            {
                FirstName = "Test User",
                LastName = "Test User's Last Name",
                NationalCode = "4100000014",
                PhoneNumber = "09123456789"
            });
        //Assert
        Assert.True(httpResult.IsSuccessStatusCode);
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var user = await dbContext.Users.FirstAsync(q =>
            q.FirstName == "Test User" &&
            q.LastName == "Test User's Last Name" &&
            q.NationalCode == "4100000014" &&
            q.PhoneNumber == "09123456789"
            );
            Assert.NotNull(user);
        }

    }
    [Fact]
    public async Task Update_User()
    {
        //Arrange
        var client = _webApplicationFactory.CreateClient();
        Guid id = Guid.Empty;
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            id = (await dbContext.Users.FirstAsync()).Id;
        }
        //Act
        var httpResult = await client.PutAsJsonAsync($"/api/v1/users/{id}",
            new UpdateUserDto()
            {
                FirstName = "Test User 1",
                LastName = "Test User's Last Name 1",
                NationalCode = "4100000014",
                PhoneNumber = "09123456789"
            });
        //Assert
        Assert.True(httpResult.IsSuccessStatusCode);
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var user = await dbContext.Users.FirstAsync(q =>
            q.FirstName == "Test User 1" &&
            q.LastName == "Test User's Last Name 1" &&
            q.NationalCode == "4100000014" &&
            q.PhoneNumber == "09123456789"
            );
            Assert.NotNull(user);
        }
    }
    [Fact]
    public async Task Get_User()
    {
        //Arrange
        var client = _webApplicationFactory.CreateClient();
        Guid id = Guid.Empty;
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            id = (await dbContext.Users.FirstAsync()).Id;
        }
        //Act
        var httpResult = await client.GetAsync($"/api/v1/users/{id}");
        //Assert
        Assert.True(httpResult.IsSuccessStatusCode);
        var user = httpResult.Content.ReadFromJsonAsync<UserDetailsDto>();
        Assert.NotNull(user);
    }

    public static IEnumerable<object[]> ValidationTestData => new List<Object[]>
    {
        new object[]{"test1", "Test1", "092141081181", "27407327081"},
        new object[]{"test1", "Test1", "09214108118", "27407327081"},
        new object[]{"test1", "Test1", "092141081181", "2740732708"},
        new object[]{"test1", "", "092141081181", "27407327081"},
        new object[]{"test1", "", "09214108118", "27407327081"},
        new object[]{"test1", "", "092141081181", "2740732708" },
    };

    [Theory]
    [MemberData(nameof(ValidationTestData))]

    public async Task Create_User_Validation(string name, string lastname, string phone, string nationalNumber)
    {
        //Arrange
        var client = _webApplicationFactory.CreateClient();
        //Act
        var httpResult = await client.PostAsJsonAsync("/api/v1/users",
            new CreateUserDto()
            {
                FirstName = name,
                LastName = lastname,
                NationalCode = phone,
                PhoneNumber = nationalNumber
            });
        //Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, httpResult.StatusCode);
    }

    [Theory]
    [MemberData(nameof(ValidationTestData))]
    public async Task Update_User_Validation(string name, string lastname, string phone, string nationalNumber)
    {
        //Arrange
        var client = _webApplicationFactory.CreateClient();
        Guid id = Guid.Empty;
        using (var scope = _webApplicationFactory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            id = (await dbContext.Users.FirstAsync()).Id;
        }
        //Act
        var httpResult = await client.PutAsJsonAsync($"/api/v1/users/{id}",
            new UpdateUserDto()
            {
                FirstName = name,
                LastName = lastname,
                NationalCode = phone,
                PhoneNumber = nationalNumber
            });
        //Assert
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, httpResult.StatusCode);
    }
}