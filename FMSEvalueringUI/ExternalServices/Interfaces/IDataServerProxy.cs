using FMSEvalueringUI.ModelDto;
using Microsoft.AspNetCore.Mvc;

namespace FMSEvalueringUI.ExternalServices.Interfaces
{
    public interface IDataServerProxy
    {
        Task<JwtTokenDto> CheckCredentials(LoginDto loginDto);
    }
}
