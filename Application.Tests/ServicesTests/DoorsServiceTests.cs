using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services;
using FluentAssertions;
using Infrastructure.Persistence.Tests.DbSetMocks;
using Moq;

namespace Application.Tests.ServicesTests
{
    public class DoorsServiceTests
    {
        private readonly IDoorsService _sut;
        private readonly Mock<IDoorsRepository> _doorsRepository;

        public DoorsServiceTests()
        {
            _doorsRepository = new Mock<IDoorsRepository>();
            _sut = new DoorsService(_doorsRepository.Object);
        }

        [Fact]
        public async Task Test_AuthorizeDoorAccessAsync_When_UserId_DoorId_Is_Valid()
        {
            // arrange
            _doorsRepository.Setup(x => x.AuthorizeDoorAccessAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(true);

            // act
            var result = await _sut.AuthorizeDoorAccessAsync(1, 1);

            // assert
            result.AccessGranted.Should().BeTrue();
            result.DoorId.Should().Be(1);
            result.UserId.Should().Be(1);
        }

        [Fact]
        public async Task Test_AuthorizeDoorAccessAsync_When_UserId_DoorId_Is_Invalid()
        {
            // arrange
            _doorsRepository.Setup(x => x.AuthorizeDoorAccessAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(false);

            // act
            var result = await _sut.AuthorizeDoorAccessAsync(3, 4);

            // assert
            result.AccessGranted.Should().BeFalse();
            result.DoorId.Should().Be(4);
            result.UserId.Should().Be(3);
        }

        [Fact]
        public async Task Test_AuthorizeViewDoorAccessHistoryAsync_When_UserId_DoorId_Is_Valid()
        {
            // arrange
            _doorsRepository.Setup(x => x.AuthorizeViewDoorAccessHistoryAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(true);

            // act
            var result = await _sut.AuthorizeViewDoorAccessHistoryAsync(1, 1);

            // assert
            result.AccessGranted.Should().BeTrue();
            result.DoorId.Should().Be(1);
            result.UserId.Should().Be(1);
        }

        [Fact]
        public async Task Test_AuthorizeViewDoorAccessHistoryAsync_When_UserId_DoorId_Is_Invalid()
        {
            // arrange
            _doorsRepository.Setup(x => x.AuthorizeViewDoorAccessHistoryAsync(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(false);

            // act
            var result = await _sut.AuthorizeViewDoorAccessHistoryAsync(3, 4);

            // assert
            result.AccessGranted.Should().BeFalse();
            result.DoorId.Should().Be(4);
            result.UserId.Should().Be(3);
        }

        [Fact]
        public async Task Test_AddDoorAccessHistoryAsync()
        {
            // arrange
            _doorsRepository.Setup(x => x.AddDoorAccessHistoryAsync(It.IsAny<long>(), It.IsAny<long>(), It.IsAny<bool>())).ReturnsAsync(true);

            // act
            var result = await _sut.AddDoorAccessHistoryAsync(1, 1, true);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Test_GetDoorAccessHistoryAsync_When_DoorId_Is_Valid()
        {
            // arrange
            var timestamp = DateTime.UtcNow;
            int doorId = 2;
            var userAccessHistoryMocks = AccessHistoryMock.GetAccessHistoryMockData(timestamp);

            _doorsRepository.Setup(x => x.GetDoorAccessHistoryAsync(It.IsAny<long>())).ReturnsAsync(userAccessHistoryMocks.Object.Where(x => x.DoorId == doorId));

            // act
            var result = await _sut.GetDoorAccessHistoryAsync(2);

            // assert
            var expectedResult = new List<AccessHistoryResponse>()
            {
                new AccessHistoryResponse{UserId=1, DoorId=2,AccessGranted=true,Timestamp=timestamp, Doorname="Storage", Username="Rameez" },
                new AccessHistoryResponse{UserId=2, DoorId=2,AccessGranted=true,Timestamp=timestamp, Doorname="Storage", Username="Darjan" }
            };
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Test_GetDoorAccessHistoryAsync_When_DoorId_Is_Invalid()
        {
            // arrange
            var timestamp = DateTime.UtcNow;
            int doorId = 5;
            var userAccessHistoryMocks = AccessHistoryMock.GetAccessHistoryMockData(timestamp);

            _doorsRepository.Setup(x => x.GetDoorAccessHistoryAsync(It.IsAny<long>())).ReturnsAsync(userAccessHistoryMocks.Object.Where(x => x.DoorId == doorId));

            // act
            var result = await _sut.GetDoorAccessHistoryAsync(5);

            // assert

            result.Should().BeNullOrEmpty();
        }
    }
}
