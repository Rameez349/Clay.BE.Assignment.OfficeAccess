using Application.Interfaces.Repositories;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Options;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Tests.DbSetMocks;
using Microsoft.Extensions.Options;
using Moq;

namespace Infrastructure.Persistence.Tests
{
    public class AccessHistoryRepositoryTests
    {
        private readonly Mock<OfficeAccessDbContext> _officeAccessDbContexMock;
        private readonly Mock<IOptions<DbOptions>> _dbOptions;
        private readonly IAccessHistoryRepository _sut;

        public AccessHistoryRepositoryTests()
        {
            _dbOptions = new Mock<IOptions<DbOptions>>();
            _dbOptions.Setup(m => m.Value).Returns(new DbOptions { ConnectionString = string.Empty });
            _officeAccessDbContexMock = new Mock<OfficeAccessDbContext>(new object[] { _dbOptions.Object });
            _sut = new AccessHistoryRepository(_officeAccessDbContexMock.Object);
        }

        [Fact]
        public async Task Test_AddAccessHistoryAsync()
        {
            //Act
            await _sut.AddAccessHistoryAsync(1, 1, true);

            //Assert
            _officeAccessDbContexMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.Run(() => { return 1; })).Verifiable();
            _officeAccessDbContexMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Test_GetAccessHistoryAsync_When_DoorId_Is_Valid()
        {
            // arrange
            var timestamp = DateTime.UtcNow;
            var userAccessLevelMocks = AccessHistoryMock.GetAccessHistoryMockData(timestamp);

            _officeAccessDbContexMock.Setup(x => x.AccessHistory).Returns(userAccessLevelMocks.Object);

            int doorId = 2;

            // act
            var result = await _sut.GetAccessHistoryAsync(doorId);

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
        public async Task Test_GetAccessHistoryAsync_When_DoorId_Is_Invalid()
        {
            // arrange
            var timestamp = DateTime.UtcNow;
            var userAccessLevelMocks = AccessHistoryMock.GetAccessHistoryMockData(timestamp);

            _officeAccessDbContexMock.Setup(x => x.AccessHistory).Returns(userAccessLevelMocks.Object);

            int doorId = 3;

            // act
            var result = await _sut.GetAccessHistoryAsync(doorId);

            // assert
            result.Should().BeNullOrEmpty();
        }
    }
}
