using System.Net;
using Blazored.LocalStorage;
using FMSEvalueringUI.ExternalServices.Interfaces;
using FMSEvalueringUI.ModelDto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FMSEvalueringUI.ExternalServices
{
    public class DataServerProxy : IDataServerProxy
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;
        public static string JwtToken { get; set; }


        public DataServerProxy(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;
        }

        async Task<JwtTokenDto> IDataServerProxy.CheckCredentials(LoginDto loginDto)
        {
            try
            {
                var requestUri = "/login";
                var response = await _httpClient.PostAsJsonAsync(requestUri, new { loginDto.Email, loginDto.Password });
                if (!response.IsSuccessStatusCode)
                {
                    //return Results.Problem("Failed to authenticate user.", statusCode: (int)response.StatusCode);
                }
                var tokenResponse = await response.Content.ReadFromJsonAsync<JwtTokenDto>(); // Adjust type as needed
                if (tokenResponse != null)
                {
                    JwtToken = tokenResponse.Value.Token;
                }

                //return Results.Ok("Successfully Logged in!");
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
