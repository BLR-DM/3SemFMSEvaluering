using System.Net;
using FMSEvalueringUI.ExternalServices.Interfaces;
using FMSEvalueringUI.ModelDto;

namespace FMSEvalueringUI.ExternalServices
{
    public class DataServerProxy : IDataServerProxy
    {
        private readonly HttpClient _httpClient;


        public DataServerProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        async Task<JwtTokenDto> IDataServerProxy.CheckCredentials(LoginDto loginDto)
        {
            try
            {
                var requestUri = "/fmsdataserver/login";
                var response = await _httpClient.PostAsJsonAsync(requestUri, new { loginDto.Email, loginDto.Password });
                if (!response.IsSuccessStatusCode)
                {
                    //return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
                }
                var content = await response.Content.ReadAsStringAsync(); // To test debug the content
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
        public string Token { get; set; }
        //public int StatusCode { get; set; }
    }

    public class ValueDto
    {
        public string Token { get; set; }
    }
}
