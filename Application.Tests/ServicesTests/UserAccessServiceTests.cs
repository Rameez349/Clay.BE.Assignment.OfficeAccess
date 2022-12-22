using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services;
using FluentAssertions;
using Moq;

namespace Application.Tests.ServicesTests
{
    public class UserAccessServiceTests
    {
        private readonly IUserAccessService _sut;
        private readonly Mock<IUserAccessRepository> _userAccessRepository;

        public UserAccessServiceTests()
        {
            _userAccessRepository = new Mock<IUserAccessRepository>();
            _sut = new UserAccessService(_userAccessRepository.Object);
        }

        [Fact]
        public async Task Test_AuthorizeUserAccessAsync_When_UserId_DoorId_Is_Valid()
        {
            // arrange
            _userAccessRepository.Setup(x => x.AuthorizeUserAccessAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            // act
            var result = await _sut.AuthorizeUserAccessAsync(1, 1);

            // assert
            result.AccessGranted.Should().BeTrue();
            result.DoorId.Should().Be(1);
            result.UserId.Should().Be(1);
        }

        [Fact]
        public async Task Test_AuthorizeUserAccessAsync_When_UserId_DoorId_Is_Invalid()
        {
            // arrange
            _userAccessRepository.Setup(x => x.AuthorizeUserAccessAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            // act
            var result = await _sut.AuthorizeUserAccessAsync(3, 4);

            // assert
            result.AccessGranted.Should().BeFalse();
            result.DoorId.Should().Be(4);
            result.UserId.Should().Be(3);
        }

        [Fact]
        public async Task Test_AuthorizeViewAccessHistoryAsync_When_UserId_DoorId_Is_Valid()
        {
            // arrange
            _userAccessRepository.Setup(x => x.AuthorizeViewAccessHistoryAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            // act
            var result = await _sut.AuthorizeViewAccessHistoryAsync(1, 1);

            // assert
            result.AccessGranted.Should().BeTrue();
            result.DoorId.Should().Be(1);
            result.UserId.Should().Be(1);
        }

        [Fact]
        public async Task Test_AuthorizeViewAccessHistoryAsync_When_UserId_DoorId_Is_Invalid()
        {
            // arrange
            _userAccessRepository.Setup(x => x.AuthorizeViewAccessHistoryAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            // act
            var result = await _sut.AuthorizeViewAccessHistoryAsync(3, 4);

            // assert
            result.AccessGranted.Should().BeFalse();
            result.DoorId.Should().Be(4);
            result.UserId.Should().Be(3);
        }
    }
}
