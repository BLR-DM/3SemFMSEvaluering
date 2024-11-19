using Gateway.API.Interfaces;

namespace Gateway.API.ExternalServices;

public class FmsProxy : IFmsProxy
{
    private readonly HttpClient _client;
    private readonly ILogger<FmsProxy> _logger;

    public FmsProxy(HttpClient client, ILogger<FmsProxy> logger)
    {
        _client = client;
        _logger = logger;
    }

    async Task<IResult> IFmsProxy.CheckCredentials(string email, string password)
    {
        try
        {
            var requestUri = "http://fmsdataserver.api:8080/fms/login";
            var response = await _client.PostAsJsonAsync(requestUri, new { email, password });

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Login failed with status code: {StatusCode}", response.StatusCode);
                return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<JwtTokenDto>(); // Adjust type as needed
            return Results.Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while checking credentials.");
            return Results.Problem("An unexpected error occurred!!!", statusCode: 500);
        }
    }

    public class JwtTokenDto
    {
        public string Token { get; set; }
    }
}