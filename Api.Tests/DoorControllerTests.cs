using Api.Tests.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using Application.Dtos.Responses;
using Application.Interfaces;
using Application.Services;
using Application.Options;
using Microsoft.Extensions.Options;
using Moq;
using Microsoft.Extensions.Configuration;

namespace Api.Tests
{
    public class DoorControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _apiClient;
        private readonly ITokenService _tokenService;
        private readonly Mock<IOptions<JwtOptions>> _mockJwtOptions;

        public DoorControllerTests(TestWebApplicationFactory factory)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets("49320df0-b1bc-4cc7-910e-b3a29f7d77ef")
                .Build();

            _mockJwtOptions = new Mock<IOptions<JwtOptions>>();
            _mockJwtOptions.Setup(m => m.Value).Returns(
                new JwtOptions
                {
                    Audience = configuration["JwtOptions:Audience"],
                    ExpirationInHours = 1,
                    Issuer = configuration["JwtOptions:Issuer"],
                    SecretKey = configuration["JwtOptions:SecretKey"]
                });

            _tokenService = new TokenService(_mockJwtOptions.Object);
            _apiClient = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
        }

        [Fact]
        public async Task Test_UnlockDoor_Returns_HttpStatus200_When_Credentials_Are_Valid()
        {
            int userId = 1;
            int doorId = 1;
            var url = $"api/Door/Unlock/{doorId}";

            var bearerToken = _tokenService.GenerateJwtToken(userId, string.Empty, true);
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            // action
            var response = await _apiClient.PostAsync(new Uri(_apiClient.BaseAddress + url), null);

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
        public async Task Test_UnlockDoor_Returns_HttpStatus200_When_Credentials_Are_Invalid()
        {
            int userId = 3;
            int doorId = 2;
            var url = $"api/Door/Unlock/{doorId}";

            var bearerToken = _tokenService.GenerateJwtToken(userId, string.Empty, true);
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            // action
            var response = await _apiClient.PostAsync(new Uri(_apiClient.BaseAddress + url), null);

            // assert
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
            var accessResponse = JsonSerializer.Deserialize<AccessResponse>(await response.Content.ReadAsStringAsync(), serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            accessResponse?.AccessGranted.Should().BeFalse();
        }

        [Fact]
        public async Task Test_AccessHistory_Returns_HttpStatus200_WithAccessHistory_When_Credentials_Are_Valid()
        {
            // arrange
            int userId = 1;
            int doortId = 1;
            var url = $"/api/Door/AccessHistory/{doortId}";

            var bearerToken = _tokenService.GenerateJwtToken(userId, string.Empty, true);
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            // action
            var response = await _apiClient.GetAsync(url);

            // assert
            var serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
            var accessHistoryResponse = JsonSerializer.Deserialize<List<AccessHistoryResponse>>(await response.Content.ReadAsStringAsync(), serializerOptions);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            accessHistoryResponse?.Should().NotBeNull();
        }

        [Fact]
        public async Task Test_AccessHistory_Returns_HttpStatus401_Unauthorized_When_Credentials_Are_Invalid()
        {
            // arrange
            int userId = 3;
            int doortId = 2;
            var url = $"/api/Door/AccessHistory/{doortId}";

            var bearerToken = _tokenService.GenerateJwtToken(userId, string.Empty, true);
            _apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

            // action
            var response = await _apiClient.GetAsync(url);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }
}
