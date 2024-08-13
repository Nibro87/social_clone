using SocialClone.Controllers;
using SocialClone.Service;
using SocialClone.Models; 

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Moq;
using Xunit;
using System.Linq;
using System.Collections.Generic;
namespace SocialClone.Tests;



public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

        public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }    
    
    
[Fact]
public async Task GetAllUsersAsync_NoUsers_ThrowsInvalidOperationException()
{
    // Arrange
    _userRepositoryMock
        .Setup(repo => repo.GetAllUsersAsync())
        .Returns(Task.FromResult<IEnumerable<User>>(Enumerable.Empty<User>()));

    // Act & Assert
    var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _userService.GetAllUsersAsync());
    Assert.Equal("The database does not contain any users!", exception.Message);
}

[Fact]

public async Task GetAllUsersAsync_WithUsers_ReturnsUserDtos()
{
    IEnumerable<User> users = new List<User>
    {
        new User { UserName = "User1" },
        new User { UserName = "User2" }
    };

    _userRepositoryMock
        .Setup(repo => repo.GetAllUsersAsync())
        .Returns(Task.FromResult(users));



    var result = await _userService.GetAllUsersAsync();

    Assert.NotNull(result);
    Assert.Equal(2,result.Count());
    Assert.NotEqual(3,result.Count());
    Assert.Contains(result, dto => dto.UserName == "User1");
    Assert.Contains(result, dto => dto.UserName == "User2");
    Assert.DoesNotContain(result, dto => dto.UserName == "User3");



}

}