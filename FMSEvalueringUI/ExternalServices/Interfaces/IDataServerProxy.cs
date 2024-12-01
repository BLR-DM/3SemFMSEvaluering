using FMSEvalueringUI.ModelDto;

namespace FMSEvalueringUI.ExternalServices.Interfaces
{
    public interface IDataServerProxy
    {
        Task<JwtTokenDto> CheckCredentials(LoginDto loginDto);
    }
}
