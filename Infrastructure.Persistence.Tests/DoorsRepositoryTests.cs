using Domain.Entities;
using Domain.RepositoryInterfaces;
using FluentAssertions;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Options;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Tests.DbSetMocks;
using Microsoft.Extensions.Options;
using Moq;

namespace Infrastructure.Persistence.Tests;

public class DoorsRepositoryTests
{
    private readonly Mock<OfficeAccessDbContext> _officeAccessDbContexMock;
    private readonly Mock<IOptions<DbOptions>> _dbOptions;
    private readonly IDoorsRepository _sut;

    public DoorsRepositoryTests()
    {
        _dbOptions = new Mock<IOptions<DbOptions>>();
        _dbOptions.Setup(m => m.Value).Returns(new DbOptions { ConnectionString = string.Empty });
        _officeAccessDbContexMock = new Mock<OfficeAccessDbContext>(new object[] { _dbOptions.Object });
        _sut = new DoorsRepository(_officeAccessDbContexMock.Object);
    }

    [Fact]
    public async Task Test_AddDoorAccessHistoryAsync()
    {
        // arrange
        var userMocks = UserMock.GetUserMockData();
        var doorMocks = DoorMock.GetDoorMockData();

        _officeAccessDbContexMock.Setup(x => x.Users).Returns(userMocks.Object);
        _officeAccessDbContexMock.Setup(x => x.Doors).Returns(doorMocks.Object);

        //Act
        await _sut.AddDoorAccessHistoryAsync(1, 1, true);

        //Assert
        _officeAccessDbContexMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.Run(() => { return 1; })).Verifiable();
        _officeAccessDbContexMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Test_GetDoorAccessHistoryAsync_When_DoorId_Is_Valid()
    {
        // arrange
        var timestamp = DateTime.UtcNow;
        var doorAccessHistoryMocks = AccessHistoryMock.GetAccessHistoryMockData(timestamp);

        _officeAccessDbContexMock.Setup(x => x.AccessHistory).Returns(doorAccessHistoryMocks.Object);

        int doorId = 2;

        // act
        var result = await _sut.GetDoorAccessHistoryAsync(doorId);

        var expectedResult = new List<AccessHistory>()
        {
            new AccessHistory { UserId = 1, DoorId = 2, AccessGranted = true, Timestamp = timestamp, Door = new Door { Id = 1, Name = "Storage" }, User = new User { Id = 1, Name = "Rameez" }},
            new AccessHistory { UserId = 2, DoorId = 2, AccessGranted = true, Timestamp = timestamp, Door = new Door { Id = 1, Name = "Storage" }, User = new User { Id = 2, Name = "Darjan" }}
        };

        // assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public async Task Test_GetDoorAccessHistoryAsync_When_DoorId_Is_Invalid()
    {
        // arrange
        var timestamp = DateTime.UtcNow;
        var doorAccessHistoryMocks = AccessHistoryMock.GetAccessHistoryMockData(timestamp);

        _officeAccessDbContexMock.Setup(x => x.AccessHistory).Returns(doorAccessHistoryMocks.Object);

        int doorId = 3;

        // act
        var result = await _sut.GetDoorAccessHistoryAsync(doorId);

        // assert
        result.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task Test_AuthorizeDoorAccessAsync_Returns_True_When_UserId_DoorId_Are_Valid()
    {
        // arrange
        var userAccessLevelMocks = UserAccessLevelMock.GetUserAccessLevelMockData();
        var doorAccessLevelMocks = DoorAccessLevelMock.GetDoorAccessLevelMockData();
        _officeAccessDbContexMock.Setup(x => x.UserAccessLevels).Returns(userAccessLevelMocks.Object);
        _officeAccessDbContexMock.Setup(x => x.DoorAccessLevels).Returns(doorAccessLevelMocks.Object);
        int userId = 1;
        int doorId = 1;

        // act
        var result = await _sut.AuthorizeDoorAccessAsync(userId, doorId);

        // assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Test_AuthorizeDoorAccessAsync_Returns_False_When_UserId_DoorId_Are_Invalid()
    {
        // arrange
        var userAccessLevelMocks = UserAccessLevelMock.GetUserAccessLevelMockData();
        var doorAccessLevelMocks = DoorAccessLevelMock.GetDoorAccessLevelMockData();
        _officeAccessDbContexMock.Setup(x => x.UserAccessLevels).Returns(userAccessLevelMocks.Object);
        _officeAccessDbContexMock.Setup(x => x.DoorAccessLevels).Returns(doorAccessLevelMocks.Object);

        int userId = 1;
        int doorId = 5;

        // act
        var result = await _sut.AuthorizeDoorAccessAsync(userId, doorId);

        // assert
        result.Should().BeFalse();
    }
}
