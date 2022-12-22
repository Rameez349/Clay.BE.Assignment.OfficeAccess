using Application.Dtos.Responses;
using Application.Mappings;
using Domain.Entities;
using FluentAssertions;

namespace Application.Tests.MappingsTests
{
    public class AccessHistoryResponseMapperTests
    {
        [Fact]
        public void Test_MapToAccessHistoryResponseDto()
        {
            DateTime timestamp = DateTime.UtcNow;
            var accessHistory = new AccessHistory
            {
                UserId = 1,
                DoorId = 1,
                AccessGranted = true,
                Timestamp = timestamp,
                Door = new Door { Id = 1, Name = "Entrance" },
                User = new User { Id = 1, Name = "Rameez" }
            };

            var accessHistoryMappedDto = accessHistory.MapToAccessHistoryResponseDto();

            var expectedResult = new AccessHistoryResponse
            {
                AccessGranted = true,
                DoorId = 1,
                UserId = 1,
                Doorname = "Entrance",
                Username = "Rameez",
                Timestamp = timestamp
            };

            accessHistoryMappedDto.Should().BeEquivalentTo(expectedResult);
        }
    }
}
