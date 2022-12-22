using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Services;
using FluentAssertions;
using Infrastructure.Persistence.Tests.DbSetMocks;
using Moq;

namespace Application.Tests.ServicesTests
{
    public class AccessHistoryServiceTests
    {
        private readonly IAccessHistoryService _sut;
        private readonly Mock<IAccessHistoryRepository> _accessHistoryRepository;

        public AccessHistoryServiceTests()
        {
            _accessHistoryRepository = new Mock<IAccessHistoryRepository>();
            _sut = new AccessHistoryService(_accessHistoryRepository.Object);
        }

        [Fact]
        public async Task Test_AddAccessHistoryAsync()
        {
            // arrange
            _accessHistoryRepository.Setup(x => x.AddAccessHistoryAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(true);

            // act
            var result = await _sut.AddAccessHistoryAsync(1, 1, true);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Test_GetAccessHistoryAsync_When_DoorId_Is_Valid()
        {
            // arrange
            var timestamp = DateTime.UtcNow;
            int doorId = 2;
            var userAccessHistoryMocks = AccessHistoryMock.GetAccessHistoryMockData(timestamp);

            _accessHistoryRepository.Setup(x => x.GetAccessHistoryAsync(It.IsAny<int>())).ReturnsAsync(userAccessHistoryMocks.Object.Where(x => x.DoorId == doorId));

            // act
            var result = await _sut.GetAccessHistoryAsync(2);

            // assert
            var expectedResult = new List<AccessHistoryResponse>()
            {
                new AccessHistoryResponse{UserId=1, DoorId=2,AccessGranted=true,Timestamp=timestamp, Doorname="Storage", Username="Rameez" },
                new AccessHistoryResponse{UserId=2, DoorId=2,AccessGranted=true,Timestamp=timestamp, Doorname="Storage", Username="Darjan" }
            };
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task Test_GetAccessHistoryAsync_When_DoorId_Is_Invalid()
        {
            // arrange
            var timestamp = DateTime.UtcNow;
            int doorId = 5;
            var userAccessHistoryMocks = AccessHistoryMock.GetAccessHistoryMockData(timestamp);

            _accessHistoryRepository.Setup(x => x.GetAccessHistoryAsync(It.IsAny<int>())).ReturnsAsync(userAccessHistoryMocks.Object.Where(x => x.DoorId == doorId));

            // act
            var result = await _sut.GetAccessHistoryAsync(5);

            // assert

            result.Should().BeNullOrEmpty();
        }
    }
}
