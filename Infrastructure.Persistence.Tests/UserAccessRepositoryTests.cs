using Application.Interfaces.Repositories;
using FluentAssertions;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Options;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Tests.DbSetMocks;
using Microsoft.Extensions.Options;
using Moq;

namespace Infrastructure.Persistence.Tests
{
    public class UserAccessRepositoryTests
    {
        private readonly Mock<OfficeAccessDbContext> _officeAccessDbContexMock;
        private readonly Mock<IOptions<DbOptions>> _dbOptions;
        private readonly IUserAccessRepository _sut;

        public UserAccessRepositoryTests()
        {
            _dbOptions = new Mock<IOptions<DbOptions>>();
            _dbOptions.Setup(m => m.Value).Returns(new DbOptions { ConnectionString = string.Empty });
            _officeAccessDbContexMock = new Mock<OfficeAccessDbContext>(new object[] { _dbOptions.Object });
            _sut = new UserAccessRepository(_officeAccessDbContexMock.Object);
        }

        [Fact]
        public async Task Test_AuthorizeUserAccessAsync_Returns_True_When_UserId_DoorId_Are_Valid()
        {
            // arrange
            var userAccessLevelMocks = UserAccessLevelMock.GetUserAccessLevelMockData();
            var doorAccessLevelMocks = DoorAccessLevelMock.GetDoorAccessLevelMockData();
            _officeAccessDbContexMock.Setup(x => x.UserAccessLevels).Returns(userAccessLevelMocks.Object);
            _officeAccessDbContexMock.Setup(x => x.DoorAccessLevels).Returns(doorAccessLevelMocks.Object);
            int userId = 1;
            int doorId = 1;

            // act
            var result = await _sut.AuthorizeUserAccessAsync(userId, doorId);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Test_AuthorizeUserAccessAsync_Returns_False_When_UserId_DoorId_Are_Invalid()
        {
            // arrange
            var userAccessLevelMocks = UserAccessLevelMock.GetUserAccessLevelMockData();
            var doorAccessLevelMocks = DoorAccessLevelMock.GetDoorAccessLevelMockData();
            _officeAccessDbContexMock.Setup(x => x.UserAccessLevels).Returns(userAccessLevelMocks.Object);
            _officeAccessDbContexMock.Setup(x => x.DoorAccessLevels).Returns(doorAccessLevelMocks.Object);

            int userId = 1;
            int doorId = 5;

            // act
            var result = await _sut.AuthorizeUserAccessAsync(userId, doorId);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Test_AuthorizeViewAccessHistoryAsync_Returns_True_When_UserId_DoorId_Are_Valid()
        {
            // arrange
            var userAccessLevelMocks = UserAccessLevelMock.GetUserAccessLevelMockData();
            var doorAccessLevelMocks = DoorAccessLevelMock.GetDoorAccessLevelMockData();
            _officeAccessDbContexMock.Setup(x => x.UserAccessLevels).Returns(userAccessLevelMocks.Object);
            _officeAccessDbContexMock.Setup(x => x.DoorAccessLevels).Returns(doorAccessLevelMocks.Object);

            int userId = 1;
            int doorId = 1;

            // act
            var result = await _sut.AuthorizeViewAccessHistoryAsync(userId, doorId);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Test_AuthorizeViewAccessHistoryAsync_Returns_False_When_UserId_DoorId_Are_Invalid()
        {
            // arrange
            var userAccessLevelMocks = UserAccessLevelMock.GetUserAccessLevelMockData();
            var doorAccessLevelMocks = DoorAccessLevelMock.GetDoorAccessLevelMockData();
            _officeAccessDbContexMock.Setup(x => x.UserAccessLevels).Returns(userAccessLevelMocks.Object);
            _officeAccessDbContexMock.Setup(x => x.DoorAccessLevels).Returns(doorAccessLevelMocks.Object);

            int userId = 2;
            int doorId = 1;

            // act
            var result = await _sut.AuthorizeViewAccessHistoryAsync(userId, doorId);

            // assert
            result.Should().BeFalse();
        }
    }
}
