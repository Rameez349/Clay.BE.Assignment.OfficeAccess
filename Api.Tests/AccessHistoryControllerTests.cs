using Api.Tests.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using Application.Dtos.Responses;

namespace Api.Tests
{
    public class AccessHistoryControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _apiClient;

        public AccessHistoryControllerTests(TestWebApplicationFactory factory)
        {
            _apiClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
        }

        [Fact]
        public async Task Test_AccessHistory_Returns_HttpStatus200_WithAccessHistory_When_Credentials_Are_Valid()
        {
            // arrange
            int userId = 1;
            int doortId = 1;
            var url = $"/api/AccessHistory/Door?UserId={userId}&DoorId={doortId}";

            // action
            var response = await _apiClient.GetAsync(url);

            // assert
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
            var accessResponse = JsonSerializer.Deserialize<List<AccessHistoryResponse>>(await response.Content.ReadAsStringAsync(), serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            accessResponse?.Should().NotBeNull();
        }

        [Fact]
        public async Task Test_AccessHistory_Returns_HttpStatus401_Unauthorized_When_Credentials_Are_Invalid()
        {
            // arrange
            int userId = 3;
            int doortId = 2;
            var url = $"/api/AccessHistory/Door?UserId={userId}&DoorId={doortId}";

            // action
            var response = await _apiClient.GetAsync(url);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
