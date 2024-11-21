using FMSEvalueringUI.ModelDto;

namespace FMSEvalueringUI.ExternalServices
{
    public class Proxy
    {
        HttpClient _client = new HttpClient();


        public async Task<JwtTokenDto> CheckCredentials(LoginDto loginDto)
        {
            try
            {
                var requestUri = "http://gateway.api:8080/login";
                var response = await _client.PostAsJsonAsync(requestUri, new { loginDto.Email, loginDto.Password });
                if (!response.IsSuccessStatusCode)
                {
                    //return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
                }
                var tokenResponse = await response.Content.ReadFromJsonAsync<JwtTokenDto>(); // Adjust type as needed
                return tokenResponse;
            }
            catch (Exception ex)
            {
                //return Results.Problem("An unexpected error occurred!!!", statusCode: 500);
                return new JwtTokenDto();
            }
        }
    }

    public class JwtTokenDto
    {
        public ValueDto Value { get; set; }
        public int StatusCode { get; set; }
    }

    public class ValueDto
    {
        public string Token { get; set; }
    }
}
