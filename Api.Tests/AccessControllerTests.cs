using Api.Tests.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using Application.Dtos.Responses;

namespace Api.Tests
{
    public class AccessControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _apiClient;

        public AccessControllerTests(TestWebApplicationFactory factory)
        {
            _apiClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
        }

        [Fact]
        public async Task Test_AccessDoor_Returns_HttpStatus200_AccessGranted_When_Credentials_Are_Valid()
        {
            // arrange
            int userId = 1;
            int doortId = 1;
            var url = $"/api/Access/Door?UserId={userId}&DoorId={doortId}";

            // action
            var response = await _apiClient.GetAsync(url);

            // assert
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
            var accessResponse = JsonSerializer.Deserialize<AccessResponse>(await response.Content.ReadAsStringAsync(), serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            accessResponse?.AccessGranted.Should().BeTrue();
        }

        [Fact]
        public async Task Test_AccessDoor_Returns_HttpStatus401_Unauthorized_When_Credentials_Are_Invalid()
        {
            // arrange
            int userId = 3;
            int doortId = 2;
            var url = $"/api/Access/Door?UserId={userId}&DoorId={doortId}";

            // action
            var response = await _apiClient.GetAsync(url);

            // assert
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
            var accessResponse = JsonSerializer.Deserialize<AccessResponse>(await response.Content.ReadAsStringAsync(), serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            accessResponse?.AccessGranted.Should().BeFalse();
        }
    }
}
